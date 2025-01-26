using Application.DTOs;
using Application.DTOs.Auth;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginDto googleLoginDto);
        Task<AuthResponseDto> RegisterBasicUserAsync(RegisterUserDto registerUserDto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        Task LogoutAsync(string username);
    }
}