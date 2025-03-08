using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs;
using Application.DTOs.Auth;
using Application.Interfaces;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly GoogleTokenValidator _googleTokenValidator;
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            IConfiguration configuration,
            GoogleTokenValidator googleTokenValidator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _googleTokenValidator = googleTokenValidator;
            _mapper = mapper;
            var token = new JwtSecurityToken();
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null || !VerifyPassword(loginDto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid username or password");

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var userDto = _mapper.Map<UserDto>(user);
            var (accessToken, accessTokenExpiration) = GenerateAccessToken(userDto);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = accessTokenExpiration,
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(refreshTokenDto.AccessToken);
            var username = principal.Identity?.Name;

            if (username == null)
                throw new UnauthorizedAccessException("Invalid access token");

            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null ||
                user.RefreshToken != refreshTokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var userDto = _mapper.Map<UserDto>(user);
            var (newAccessToken, accessTokenExpiration) = GenerateAccessToken(userDto);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiration = accessTokenExpiration,
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task LogoutAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await _userRepository.UpdateAsync(user);
            }
        }

        private (string Token, DateTime Expiration) GenerateAccessToken(UserDto userDto)
        {
            var roleString = userDto.Role?.ToString() ?? "Customer"; // Default to "Customer" if Role is null

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userDto.Username),
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Role, roleString) // Store role as string
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:SecretKey"] ??
                throw new InvalidOperationException("JWT Secret Key is not configured")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"] ?? "30"));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ??
                    throw new InvalidOperationException("JWT Secret Key is not configured"))),
                ValidateLifetime = false, 
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return principal;
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }
        
        public async Task<AuthResponseDto> RegisterBasicUserAsync(RegisterUserDto registerUserDto)
        {
            if (await _userRepository.UsernameExistsAsync(registerUserDto.Username))
                throw new InvalidOperationException("Username already exists");

            if (await _userRepository.EmailExistsAsync(registerUserDto.Email))
                throw new InvalidOperationException("Email already exists");

            var user = new User
            {
                Username = registerUserDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                Email = registerUserDto.Email,
                Phone = registerUserDto.Phone,
                FullName = registerUserDto.FullName ?? registerUserDto.Username,
                Role = (int?)UserRole.Customer,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);

            var userDto = _mapper.Map<UserDto>(user);
            var (accessToken, accessTokenExpiration) = GenerateAccessToken(userDto);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = accessTokenExpiration,
                User = _mapper.Map<UserDto>(user)
            };
        }
        
        public async Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginDto googleLoginDto)
        {
            var payload = await _googleTokenValidator.ValidateTokenAsync(
                googleLoginDto.IdToken,
                googleLoginDto.ClientId
            );

            var user = await _userRepository.GetByEmailAsync(payload.Email);

            if (user == null)
            {
                user = new User
                {
                    Email = payload.Email,
                    FullName = payload.Name,
                    Role = (int?)UserRole.Customer,
                    CreatedAt = DateTime.UtcNow
                };

                await _userRepository.CreateAsync(user);
            }

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var userDto = _mapper.Map<UserDto>(user);
            var (accessToken, accessTokenExpiration) = GenerateAccessToken(userDto);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = accessTokenExpiration,
                User = _mapper.Map<UserDto>(user)
            };
        }
    }
}