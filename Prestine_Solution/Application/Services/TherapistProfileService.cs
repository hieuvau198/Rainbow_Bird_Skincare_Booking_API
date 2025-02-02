using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TherapistProfileService : ITherapistProfileService
    {
        private readonly IGenericRepository<TherapistProfile> _repository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public TherapistProfileService(
            IGenericRepository<TherapistProfile> repository,
            IImageService imageService,
            IMapper mapper)
        {
            _repository = repository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<TherapistProfileDto> GetProfileByTherapistIdAsync(int therapistId)
        {
            var profiles = await _repository.GetAllAsync();
            var profile = profiles.FirstOrDefault(p => p.TherapistId == therapistId);

            if (profile == null)
                throw new KeyNotFoundException($"Profile not found for therapist ID: {therapistId}");

            return _mapper.Map<TherapistProfileDto>(profile);
        }

        public async Task<TherapistProfileDto> CreateProfileAsync(int therapistId, CreateTherapistProfileDto dto)
        {
            // Check if profile already exists
            var existingProfiles = await _repository.GetAllAsync();
            if (existingProfiles.Any(p => p.TherapistId == therapistId))
                throw new InvalidOperationException($"Profile already exists for therapist ID: {therapistId}");

            var profile = _mapper.Map<TherapistProfile>(dto);
            profile.TherapistId = therapistId;
            profile.CreatedAt = DateTime.UtcNow;
            profile.UpdatedAt = DateTime.UtcNow;

            // Handle profile image
            if (dto.ProfileImage != null)
            {
                profile.ProfileImage = await _imageService.ConvertToBase64Async(dto.ProfileImage);
            }

            var createdProfile = await _repository.CreateAsync(profile);
            return _mapper.Map<TherapistProfileDto>(createdProfile);
        }

        public async Task<TherapistProfileDto> UpdateProfileAsync(int therapistId, UpdateTherapistProfileDto dto)
        {
            var profiles = await _repository.GetAllAsync();
            var existingProfile = profiles.FirstOrDefault(p => p.TherapistId == therapistId);

            if (existingProfile == null)
                throw new KeyNotFoundException($"Profile not found for therapist ID: {therapistId}");

            // Handle profile image update
            if (dto.ProfileImage != null)
            {
                existingProfile.ProfileImage = await _imageService.ConvertToBase64Async(dto.ProfileImage);
            }

            // Update other properties
            _mapper.Map(dto, existingProfile);
            existingProfile.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existingProfile);
            return _mapper.Map<TherapistProfileDto>(existingProfile);
        }

        public async Task DeleteProfileAsync(int therapistId)
        {
            var profiles = await _repository.GetAllAsync();
            var profile = profiles.FirstOrDefault(p => p.TherapistId == therapistId);

            if (profile == null)
                throw new KeyNotFoundException($"Profile not found for therapist ID: {therapistId}");

            await _repository.DeleteAsync(profile);
        }

        public async Task<IEnumerable<TherapistProfileDto>> GetAllProfilesAsync()
        {
            var profiles = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TherapistProfileDto>>(profiles);
        }
    }
}
