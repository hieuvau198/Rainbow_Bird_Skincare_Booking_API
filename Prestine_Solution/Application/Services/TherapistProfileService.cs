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
        public async Task<IEnumerable<TherapistProfileDto>> GetAllProfilesAsync()
        {
            var profiles = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TherapistProfileDto>>(profiles);
        }

        public async Task<IEnumerable<TherapistProfileDto>> GetAllProfilesWithReferenceAsync()
        {
            var profiles = await _repository.GetAllAsync(null, t => t.Therapist, t => t.Therapist.User);
            return _mapper.Map<IEnumerable<TherapistProfileDto>>(profiles);
        }
        
        public async Task<TherapistProfileDto> GetProfileByProfileIdAsync(int profileId)
        {
            var profile = await _repository.GetByIdAsync(profileId);

            if (profile == null)
                throw new KeyNotFoundException($"Profile not found for Profile ID: {profileId}");

            return _mapper.Map<TherapistProfileDto>(profile);
        }

        public async Task<TherapistProfileDto> GetProfileWithReferenceByProfileIdAsync(int profileId)
        {
            var profile = await _repository.GetByIdAsync(profileId, p => p.Therapist, p => p.Therapist.User);

            if (profile == null)
                throw new KeyNotFoundException($"Profile not found for Profile ID: {profileId}");

            return _mapper.Map<TherapistProfileDto>(profile);
        }

        public async Task<TherapistProfileDto> GetProfileByTherapistIdAsync(int therapistId)
        {
            var profile = await _repository.FindAsync(p => p.TherapistId == therapistId);

            if (profile == null)
                throw new KeyNotFoundException($"Profile not found for therapist ID: {therapistId}");

            return _mapper.Map<TherapistProfileDto>(profile);
        }

        public async Task<TherapistProfileDto> GetProfileWithReferenceByTherapistIdAsync(int therapistId)
        {
            var profile = await _repository.FindAsync(
                p => p.TherapistId == therapistId,
                p => p.Therapist,
                p => p.Therapist.User
            );

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
                profile.ProfileImage = await _imageService.UploadImageAsync(dto.ProfileImage);
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
                // Delete old image if it exists
                if (!string.IsNullOrEmpty(existingProfile.ProfileImage))
                {
                    await _imageService.DeleteImageAsync(existingProfile.ProfileImage);
                }

                existingProfile.ProfileImage = await _imageService.UploadImageAsync(dto.ProfileImage);
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

            // Delete profile image if it exists
            if (!string.IsNullOrEmpty(profile.ProfileImage))
            {
                await _imageService.DeleteImageAsync(profile.ProfileImage);
            }

            await _repository.DeleteAsync(profile);
        }
        
    }
}
