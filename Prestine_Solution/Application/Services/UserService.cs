﻿using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(
            IGenericRepository<User> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _repository.FindAsync(u => u.Username == username); // ✅ Filtering in DB

            if (user == null)
                throw new KeyNotFoundException($"User with username {username} not found");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _repository.FindAsync(u => u.Email.ToLower() == email.ToLower()); // ✅ Filtering in DB

            if (user == null)
                throw new KeyNotFoundException($"User with email {email} not found");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            // Optimized: Use ExistsAsync instead of fetching all users
            if (await _repository.ExistsAsync(u => u.Username == createUserDto.Username))
                throw new InvalidOperationException("Username already exists");

            if (await _repository.ExistsAsync(u => u.Email.ToLower() == createUserDto.Email.ToLower()))
                throw new InvalidOperationException("Email already exists");

            var user = _mapper.Map<User>(createUserDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
            user.Role = (int?)(createUserDto.Role ?? UserRole.Customer);
            user.CreatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            _mapper.Map(updateUserDto, user);
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            await _repository.DeleteAsync(user);
        }
    }
}