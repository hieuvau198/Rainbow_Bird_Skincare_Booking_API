using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFeedbackAnswerService
    {
        Task<IEnumerable<FeedbackAnswerDto>> GetAllAnswersAsync();
        Task<FeedbackAnswerDto> GetAnswerByIdAsync(int id);
        Task<IEnumerable<FeedbackAnswerDto>> GetAnswersByQuestionIdAsync(int questionId);
        Task<FeedbackAnswerDto> CreateAnswerAsync(CreateFeedbackAnswerDto createDto);
        Task UpdateAnswerAsync(int id, UpdateFeedbackAnswerDto updateDto);
        Task DeleteAnswerAsync(int id);
        Task<bool> AnswerExistsAsync(int id);
        Task<int> CountAnswersByQuestionIdAsync(int questionId);
    }
}