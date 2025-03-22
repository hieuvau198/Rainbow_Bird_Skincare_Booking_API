using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HashtagService : IHashtagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HashtagService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<HashtagDto> GetHashtagByIdAsync(int hashtagId)
        {
            var hashtag = await _unitOfWork.Hashtags.GetByIdAsync(hashtagId);
            if (hashtag == null)
                throw new KeyNotFoundException($"Hashtag not found with ID: {hashtagId}");

            return _mapper.Map<HashtagDto>(hashtag);
        }

        public async Task<IEnumerable<HashtagDto>> GetAllHashtagsAsync()
        {
            var hashtags = await _unitOfWork.Hashtags.GetAllAsync();
            return _mapper.Map<IEnumerable<HashtagDto>>(hashtags);
        }

        public async Task<HashtagDto> CreateHashtagAsync(CreateHashtagDto dto)
        {
            // Check if hashtag with the same name already exists
            bool exists = await _unitOfWork.Hashtags.ExistsAsync(h => h.Name == dto.Name);
            if (exists)
                throw new ArgumentException($"Hashtag with name '{dto.Name}' already exists");

            var hashtag = _mapper.Map<Hashtag>(dto);
            hashtag.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Hashtags.CreateAsync(hashtag);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<HashtagDto>(hashtag);
        }

        public async Task<HashtagDto> UpdateHashtagAsync(int hashtagId, UpdateHashtagDto dto)
        {
            var existingHashtag = await _unitOfWork.Hashtags.GetByIdAsync(hashtagId);
            if (existingHashtag == null)
                throw new KeyNotFoundException($"Hashtag not found with ID: {hashtagId}");

            // Check if the new name already exists (if name is being changed)
            if (dto.Name != existingHashtag.Name)
            {
                bool exists = await _unitOfWork.Hashtags.ExistsAsync(h => h.Name == dto.Name);
                if (exists)
                    throw new ArgumentException($"Hashtag with name '{dto.Name}' already exists");
            }

            _mapper.Map(dto, existingHashtag);
            await _unitOfWork.Hashtags.UpdateAsync(existingHashtag);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<HashtagDto>(existingHashtag);
        }

        public async Task DeleteHashtagAsync(int hashtagId)
        {
            var hashtag = await _unitOfWork.Hashtags.GetByIdAsync(hashtagId);
            if (hashtag == null)
                throw new KeyNotFoundException($"Hashtag not found with ID: {hashtagId}");

            await _unitOfWork.Hashtags.DeleteAsync(hashtag);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}