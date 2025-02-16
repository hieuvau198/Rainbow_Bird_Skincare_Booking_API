using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizDto>> GetAllQuizzesAsync();
        Task<QuizDto> GetQuizByIdAsync(int id);
        Task<QuizDto> CreateQuizAsync(CreateQuizDto createDto);
        Task UpdateQuizAsync(int id, UpdateQuizDto updateDto);
        Task DeleteQuizAsync(int id);
        Task<IEnumerable<QuizDto>> GetActiveQuizzesAsync();
        Task<IEnumerable<QuizDto>> GetQuizzesByCategoryAsync(string category);
    }
}
