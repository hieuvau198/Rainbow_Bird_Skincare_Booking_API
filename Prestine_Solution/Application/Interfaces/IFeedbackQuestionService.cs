using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFeedbackQuestionService
    {
        Task<IEnumerable<FeedbackQuestionDto>> GetAllQuestionsAsync();
        Task<FeedbackQuestionDto> GetQuestionByIdAsync(int id);
        Task<FeedbackQuestionDto> CreateQuestionAsync(CreateFeedbackQuestionDto createDto);
        Task UpdateQuestionAsync(int id, UpdateFeedbackQuestionDto updateDto);
        Task DeleteQuestionAsync(int id);
        Task<bool> QuestionExistsAsync(int id);
        Task<IEnumerable<FeedbackQuestionDto>> SearchQuestionsAsync(string searchTerm);
    }
}