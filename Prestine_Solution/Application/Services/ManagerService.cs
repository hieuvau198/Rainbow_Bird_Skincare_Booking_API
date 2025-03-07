using Application.DTOs;
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
    public class ManagerService : IManagerService
    {
        private readonly IGenericRepository<Manager> _repository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public ManagerService(
            IGenericRepository<Manager> repository,
            IGenericRepository<User> userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ManagerDto>> GetAllManagersAsync()
        {
            var managers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ManagerDto>>(managers);
        }

        public async Task<ManagerDto> GetManagerByIdAsync(int id)
        {
            var manager = await _repository.GetByIdAsync(id);
            if (manager == null)
                throw new KeyNotFoundException($"Manager with ID {id} not found");

            return _mapper.Map<ManagerDto>(manager);
        }

        public async Task<ManagerDto> GetManagerByUserIdAsync(int userId)
        {
            var managers = await _repository.GetAllAsync();
            var manager = managers.FirstOrDefault(m => m.UserId == userId);

            if (manager == null)
                throw new KeyNotFoundException($"Manager with User ID {userId} not found");

            return _mapper.Map<ManagerDto>(manager);
        }

        public async Task<ManagerDto> CreateManagerAsync(CreateManagerDto createDto)
        {
            var manager = _mapper.Map<Manager>(createDto);
            await _repository.CreateAsync(manager);
            return _mapper.Map<ManagerDto>(manager);
        }

        public async Task<ManagerDto> CreateManagerWithUserAsync(CreateManagerUserDto createDto)
        {
            // Check if user already exists
            var users = await _userRepository.GetAllAsync();
            if (users.Any(u => u.Email.ToLower() == createDto.Email.ToLower()))
                throw new InvalidOperationException("A user with this email already exists.");

            // Create a new user
            var user = new User
            {
                Username = createDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(createDto.Password),
                Email = createDto.Email,
                Phone = createDto.Phone,
                FullName = createDto.FullName,
                Role = (int?)UserRole.Manager,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);

            // Create the manager linked to the user
            var manager = new Manager
            {
                UserId = user.UserId,
                Department = createDto.Department,
                Responsibilities = createDto.Responsibilities,
                HireDate = createDto.HireDate
            };

            await _repository.CreateAsync(manager);

            return _mapper.Map<ManagerDto>(manager);
        }

        public async Task UpdateManagerAsync(int id, UpdateManagerDto updateDto)
        {
            var manager = await _repository.GetByIdAsync(id);
            if (manager == null)
                throw new KeyNotFoundException($"Manager with ID {id} not found");

            _mapper.Map(updateDto, manager);
            await _repository.UpdateAsync(manager);
        }

        public async Task DeleteManagerAsync(int id)
        {
            var manager = await _repository.GetByIdAsync(id);
            if (manager == null)
                throw new KeyNotFoundException($"Manager with ID {id} not found");

            await _repository.DeleteAsync(manager);
        }
    }
}
