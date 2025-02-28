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
    public class TherapistService : ITherapistService
    {
        private readonly IGenericRepository<Therapist> _repository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public TherapistService(
            IGenericRepository<Therapist> repository, 
            IGenericRepository<User> userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<TherapistDto>> GetTherapistsAsync()
        {
            var therapists = await _repository.GetAllAsync(t => t.User);
            return _mapper.Map<IEnumerable<TherapistDto>>(therapists);
        }

        public async Task<IEnumerable<TherapistDto>> GetTherapistsWithReferenceAsync()
        {
            var therapists = await _repository.GetAllAsync(t => t.User);
            return _mapper.Map<IEnumerable<TherapistDto>>(therapists);
        }

        public async Task<TherapistDto> GetByIdAsync(int id)
        {
            var therapist = await _repository.GetByIdAsync(id);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            return _mapper.Map<TherapistDto>(therapist);
        }

        public async Task<TherapistDto> GetByIdWithReferenceAsync(int id)
        {
            var therapist = await _repository.GetByIdAsync(id, t => t.User);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            return _mapper.Map<TherapistDto>(therapist);
        }

        public async Task<TherapistDto> CreateTherapistAsync(CreateTherapistDto createDto)
        {
            var therapist = _mapper.Map<Therapist>(createDto);
            await _repository.CreateAsync(therapist);
            return _mapper.Map<TherapistDto>(therapist);
        }
        public async Task<TherapistDto> CreateTherapistWithUserAsync(CreateTherapistUserDto createDto)
        {
            // Check if the user already exists (by email)
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
                Role = UserRole.Therapist,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);

            // Create the therapist linked to the new user
            var therapist = new Therapist
            {
                UserId = user.UserId, // Link therapist to the created user
                IsAvailable = createDto.IsAvailable,
                Schedule = createDto.Schedule,
                Rating = createDto.Rating
            };

            await _repository.CreateAsync(therapist);

            return _mapper.Map<TherapistDto>(therapist);
        }

        public async Task UpdateTherapistAsync(int id, UpdateTherapistDto updateDto)
        {
            var therapist = await _repository.GetByIdAsync(id);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            _mapper.Map(updateDto, therapist);
            await _repository.UpdateAsync(therapist);
        }

        public async Task DeleteTherapistAsync(int id)
        {
            var therapist = await _repository.GetByIdAsync(id);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            await _repository.DeleteAsync(therapist);
        }
    }
}