using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQuizRecommendationService
    {
        Task<IEnumerable<QuizRecommendationDto>> GetAllRecommendationsAsync();
        Task<QuizRecommendationDto> GetRecommendationByIdAsync(int id);
        Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByQuizIdAsync(int quizId);
        Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByServiceIdAsync(int serviceId);
        Task<QuizRecommendationDto> CreateRecommendationAsync(CreateQuizRecommendationDto createDto);
        Task UpdateRecommendationAsync(int id, UpdateQuizRecommendationDto updateDto);
        Task DeleteRecommendationAsync(int id);
        Task<IEnumerable<QuizRecommendationDto>> GetActiveRecommendationsAsync();
    }
}
