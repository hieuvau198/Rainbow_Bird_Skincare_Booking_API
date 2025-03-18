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
        private readonly IGenericRepository<Hashtag> _repository;
        private readonly IMapper _mapper;

        public HashtagService(
            IGenericRepository<Hashtag> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HashtagDto> GetHashtagByIdAsync(int hashtagId)
        {
            var hashtag = await _repository.GetByIdAsync(hashtagId);
            if (hashtag == null)
                throw new KeyNotFoundException($"Hashtag not found with ID: {hashtagId}");

            return _mapper.Map<HashtagDto>(hashtag);
        }

        public async Task<IEnumerable<HashtagDto>> GetAllHashtagsAsync()
        {
            var hashtags = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<HashtagDto>>(hashtags);
        }

        public async Task<HashtagDto> CreateHashtagAsync(CreateHashtagDto dto)
        {
            // Check if hashtag with the same name already exists
            bool exists = await _repository.ExistsAsync(h => h.Name == dto.Name);
            if (exists)
                throw new ArgumentException($"Hashtag with name '{dto.Name}' already exists");

            var hashtag = _mapper.Map<Hashtag>(dto);
            hashtag.CreatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(hashtag);
            return _mapper.Map<HashtagDto>(hashtag);
        }

        public async Task<HashtagDto> UpdateHashtagAsync(int hashtagId, UpdateHashtagDto dto)
        {
            var existingHashtag = await _repository.GetByIdAsync(hashtagId);
            if (existingHashtag == null)
                throw new KeyNotFoundException($"Hashtag not found with ID: {hashtagId}");

            // Check if the new name already exists (if name is being changed)
            if (dto.Name != existingHashtag.Name)
            {
                bool exists = await _repository.ExistsAsync(h => h.Name == dto.Name);
                if (exists)
                    throw new ArgumentException($"Hashtag with name '{dto.Name}' already exists");
            }

            _mapper.Map(dto, existingHashtag);
            await _repository.UpdateAsync(existingHashtag);

            return _mapper.Map<HashtagDto>(existingHashtag);
        }

        public async Task DeleteHashtagAsync(int hashtagId)
        {
            var hashtag = await _repository.GetByIdAsync(hashtagId);
            if (hashtag == null)
                throw new KeyNotFoundException($"Hashtag not found with ID: {hashtagId}");

            await _repository.DeleteAsync(hashtag);
        }
    }
}