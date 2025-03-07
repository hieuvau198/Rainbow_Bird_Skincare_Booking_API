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
    public class StaffService : IStaffService
    {
        private readonly IGenericRepository<Staff> _repository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public StaffService(
            IGenericRepository<Staff> repository,
            IGenericRepository<User> userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StaffDto>> GetAllStaffAsync()
        {
            var staff = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<StaffDto>>(staff);
        }

        public async Task<StaffDto> GetStaffByIdAsync(int id)
        {
            var staff = await _repository.GetByIdAsync(id);
            if (staff == null)
                throw new KeyNotFoundException($"Staff with ID {id} not found");

            return _mapper.Map<StaffDto>(staff);
        }

        public async Task<StaffDto> GetStaffByUserIdAsync(int userId)
        {
            var staffList = await _repository.GetAllAsync();
            var staff = staffList.FirstOrDefault(s => s.UserId == userId);

            if (staff == null)
                throw new KeyNotFoundException($"No staff found for User ID {userId}");

            return _mapper.Map<StaffDto>(staff);
        }

        public async Task<StaffDto> CreateStaffAsync(CreateStaffDto createDto)
        {
            var staff = _mapper.Map<Staff>(createDto);
            await _repository.CreateAsync(staff);
            return _mapper.Map<StaffDto>(staff);
        }

        public async Task<StaffDto> CreateStaffWithUserAsync(CreateStaffUserDto createDto)
        {
            // Check if the user already exists
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
                Role = (int?)UserRole.Staff,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);

            // Create the staff linked to the user
            var staff = new Staff
            {
                UserId = user.UserId,
                Department = createDto.Department,
                Position = createDto.Position,
                HireDate = createDto.HireDate
            };

            await _repository.CreateAsync(staff);

            return _mapper.Map<StaffDto>(staff);
        }

        public async Task UpdateStaffAsync(int id, UpdateStaffDto updateDto)
        {
            var staff = await _repository.GetByIdAsync(id);
            if (staff == null)
                throw new KeyNotFoundException($"Staff with ID {id} not found");

            _mapper.Map(updateDto, staff);
            await _repository.UpdateAsync(staff);
        }

        public async Task DeleteStaffAsync(int id)
        {
            var staff = await _repository.GetByIdAsync(id);
            if (staff == null)
                throw new KeyNotFoundException($"Staff with ID {id} not found");

            await _repository.DeleteAsync(staff);
        }
    }
}
