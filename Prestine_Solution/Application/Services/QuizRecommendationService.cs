using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        // Helper method to determine whether to include related entities
        private static Expression<Func<QuizRecommendation, object>>[] GetIncludes(bool includeReferences)
        {
            return includeReferences
                ? new Expression<Func<QuizRecommendation, object>>[] { r => r.Quiz, r => r.Service }
                : Array.Empty<Expression<Func<QuizRecommendation, object>>>();
        }

        // Get All - Option to include related entities
        public async Task<IEnumerable<QuizRecommendationDto>> GetAllRecommendationsAsync(bool includeReferences = false)
        {
            var recommendations = await _repository.GetAllAsync(null, GetIncludes(includeReferences));
            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(recommendations);
        }

        // Get by ID - Optionally includes related entities
        public async Task<QuizRecommendationDto> GetRecommendationByIdAsync(int id, bool includeReferences = false)
        {
            var recommendation = await _repository.GetByIdAsync(id, GetIncludes(includeReferences));
            if (recommendation == null)
                throw new KeyNotFoundException($"QuizRecommendation with ID {id} not found");

            return _mapper.Map<QuizRecommendationDto>(recommendation);
        }

        // Get by Quiz ID - Filters by quizId, optionally includes related entities
        public async Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByQuizIdAsync(int quizId, bool includeReferences = false)
        {
            var recommendations = await _repository.GetAllAsync(r => r.QuizId == quizId, GetIncludes(includeReferences));
            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(recommendations);
        }

        // Get by Score (Filters by active status as well)
        public async Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByScoreAsync(int quizId, int score, bool includeReferences = false)
        {
            var recommendations = await _repository.GetAllAsync(
                r => r.QuizId == quizId && r.MinScore <= score && r.MaxScore >= score && r.IsActive == true,
                GetIncludes(includeReferences));

            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(recommendations);
        }

        // Get by Service ID - Filters by serviceId, optionally includes related entities
        public async Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByServiceIdAsync(int serviceId, bool includeReferences = false)
        {
            var recommendations = await _repository.GetAllAsync(r => r.ServiceId == serviceId, GetIncludes(includeReferences));
            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(recommendations);
        }

        // Get Active Recommendations - Filters by IsActive, optionally includes related entities
        public async Task<IEnumerable<QuizRecommendationDto>> GetActiveRecommendationsAsync(bool includeReferences = false)
        {
            var recommendations = await _repository.GetAllAsync(r => r.IsActive == true, GetIncludes(includeReferences));
            return _mapper.Map<IEnumerable<QuizRecommendationDto>>(recommendations);
        }

        // Create Recommendation
        public async Task<QuizRecommendationDto> CreateRecommendationAsync(CreateQuizRecommendationDto createDto)
        {
            var recommendation = _mapper.Map<QuizRecommendation>(createDto);
            recommendation.CreatedAt = DateTime.UtcNow;
            recommendation.IsActive = true;

            await _repository.CreateAsync(recommendation);
            return _mapper.Map<QuizRecommendationDto>(recommendation);
        }

        // Update Recommendation
        public async Task UpdateRecommendationAsync(int id, UpdateQuizRecommendationDto updateDto)
        {
            var recommendation = await _repository.GetByIdAsync(id);
            if (recommendation == null)
                throw new KeyNotFoundException($"QuizRecommendation with ID {id} not found");

            _mapper.Map(updateDto, recommendation);
            await _repository.UpdateAsync(recommendation);
        }

        // Delete Recommendation
        public async Task DeleteRecommendationAsync(int id)
        {
            var recommendation = await _repository.GetByIdAsync(id);
            if (recommendation == null)
                throw new KeyNotFoundException($"QuizRecommendation with ID {id} not found");

            await _repository.DeleteAsync(recommendation);
        }
    }
}
