using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync();
        Task<QuestionDto> GetQuestionByIdAsync(int id);
        Task<IEnumerable<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId);
        Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto createDto);
        Task UpdateQuestionAsync(int id, UpdateQuestionDto updateDto);
        Task DeleteQuestionAsync(int id);
        Task<IEnumerable<QuestionDto>> GetActiveQuestionsByQuizIdAsync(int quizId);
    }
}
