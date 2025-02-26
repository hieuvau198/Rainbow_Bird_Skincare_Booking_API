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
    public class QuizRecommendationService : IQuizRecommendationService
    {
        private readonly IGenericRepository<QuizRecommendation> _repository;
        private readonly IMapper _mapper;

        public QuizRecommendationService(
            IGenericRepository<QuizRecommendation> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuizRecommendationDto>> GetAllRecommendationsAsync()
        {
            var recommendations = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(recommendations);
        }

        public async Task<QuizRecommendationDto> GetRecommendationByIdAsync(int id)
        {
            var recommendation = await _repository.GetByIdAsync(id);
            if (recommendation == null)
                throw new KeyNotFoundException($"QuizRecommendation with ID {id} not found");

            return _mapper.Map<QuizRecommendationDto>(recommendation);
        }

        public async Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByQuizIdAsync(int quizId)
        {
            var recommendations = await _repository.GetAllAsync();
            var filtered = recommendations.Where(r => r.QuizId == quizId);
            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(filtered);
        }

        public async Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByScoreAsync(int quizId, int score)
        {
            var recommendations = await _repository.GetAllAsync();
            var filtered = recommendations
                .Where(r => r.QuizId == quizId && r.MinScore <= score && r.MaxScore >= score && r.IsActive == true)
                .ToList();

            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(filtered);
        }


        public async Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByServiceIdAsync(int serviceId)
        {
            var recommendations = await _repository.GetAllAsync();
            var filtered = recommendations.Where(r => r.ServiceId == serviceId);
            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(filtered);
        }

        public async Task<QuizRecommendationDto> CreateRecommendationAsync(CreateQuizRecommendationDto createDto)
        {
            var recommendation = _mapper.Map<QuizRecommendation>(createDto);
            recommendation.CreatedAt = DateTime.UtcNow;
            recommendation.IsActive = true;

            await _repository.CreateAsync(recommendation);
            return _mapper.Map<QuizRecommendationDto>(recommendation);
        }

        public async Task UpdateRecommendationAsync(int id, UpdateQuizRecommendationDto updateDto)
        {
            var recommendation = await _repository.GetByIdAsync(id);
            if (recommendation == null)
                throw new KeyNotFoundException($"QuizRecommendation with ID {id} not found");

            _mapper.Map(updateDto, recommendation);
            await _repository.UpdateAsync(recommendation);
        }

        public async Task DeleteRecommendationAsync(int id)
        {
            var recommendation = await _repository.GetByIdAsync(id);
            if (recommendation == null)
                throw new KeyNotFoundException($"QuizRecommendation with ID {id} not found");

            await _repository.DeleteAsync(recommendation);
        }

        public async Task<IEnumerable<QuizRecommendationDto>> GetActiveRecommendationsAsync()
        {
            var recommendations = await _repository.GetAllAsync();
            var filtered = recommendations.Where(r => r.IsActive == true);
            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(filtered);
        }
    }
}
