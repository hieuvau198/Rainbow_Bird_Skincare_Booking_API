using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAnswerService
    {
        Task<IEnumerable<AnswerDto>> GetAllAnswersAsync();
        Task<AnswerDto> GetAnswerByIdAsync(int id);
        Task<IEnumerable<AnswerDto>> GetAnswersByQuestionIdAsync(int questionId);
        Task<AnswerDto> CreateAnswerAsync(CreateAnswerDto createDto);
        Task UpdateAnswerAsync(int id, UpdateAnswerDto updateDto);
        Task DeleteAnswerAsync(int id);
    }
}
