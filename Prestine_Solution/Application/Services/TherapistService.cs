using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
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
        private readonly ITherapistRepository _therapistRepository;
        private readonly IUserService _userService;

        public TherapistService(ITherapistRepository therapistRepository, IUserService userService)
        {
            _therapistRepository = therapistRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<TherapistDto>> GetAllTherapistsAsync()
        {
            var therapists = await _therapistRepository.GetAllAsync();
            return therapists.Select(MapToDto);
        }

        public async Task<TherapistDto> GetTherapistByIdAsync(int id)
        {
            var therapist = await _therapistRepository.GetByIdAsync(id);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            return MapToDto(therapist);
        }

        public async Task<TherapistDto> CreateTherapistAsync(CreateTherapistDto createTherapistDto)
        {
            // Check if user exists
            var user = await _userService.GetUserByIdAsync(createTherapistDto.UserId);

            // Check if therapist already exists for this user
            var existingTherapist = await _therapistRepository.GetByUserIdAsync(createTherapistDto.UserId);
            if (existingTherapist != null)
                throw new InvalidOperationException($"Therapist already exists for user with ID {createTherapistDto.UserId}");

            var therapist = new Therapist
            {
                UserId = createTherapistDto.UserId,
                IsAvailable = createTherapistDto.IsAvailable,
                Schedule = createTherapistDto.Schedule
            };

            await _therapistRepository.CreateAsync(therapist);
            return MapToDto(therapist);
        }

        public async Task UpdateTherapistAsync(int id, UpdateTherapistDto updateTherapistDto)
        {
            var therapist = await _therapistRepository.GetByIdAsync(id);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            if (updateTherapistDto.IsAvailable.HasValue)
                therapist.IsAvailable = updateTherapistDto.IsAvailable;
            if (updateTherapistDto.Schedule != null)
                therapist.Schedule = updateTherapistDto.Schedule;

            await _therapistRepository.UpdateAsync(therapist);
        }

        public async Task DeleteTherapistAsync(int id)
        {
            var therapist = await _therapistRepository.GetByIdAsync(id);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            await _therapistRepository.DeleteAsync(therapist);
        }

        private TherapistDto MapToDto(Therapist therapist)
        {
            return new TherapistDto
            {
                TherapistId = therapist.TherapistId,
                UserId = therapist.UserId,
                IsAvailable = therapist.IsAvailable,
                Schedule = therapist.Schedule,
                Rating = therapist.Rating,
                User = _userService.MapToDto(therapist.User)
            };
        }
    }
}
