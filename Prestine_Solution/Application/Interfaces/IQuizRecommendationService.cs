using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQuizRecommendationService
    {
        Task<IEnumerable<QuizRecommendationDto>> GetAllRecommendationsAsync(bool includeReferences = false);
        Task<QuizRecommendationDto> GetRecommendationByIdAsync(int id, bool includeReferences = false);
        Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByQuizIdAsync(int quizId, bool includeReferences = false);
        Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByScoreAsync(int quizId, int score, bool includeReferences = false);
        Task<IEnumerable<QuizRecommendationDto>> GetRecommendationsByServiceIdAsync(int serviceId, bool includeReferences = false);
        Task<IEnumerable<QuizRecommendationDto>> GetActiveRecommendationsAsync(bool includeReferences = false);

        Task<QuizRecommendationDto> CreateRecommendationAsync(CreateQuizRecommendationDto createDto);
        Task UpdateRecommendationAsync(int id, UpdateQuizRecommendationDto updateDto);
        Task DeleteRecommendationAsync(int id);
    }
}
