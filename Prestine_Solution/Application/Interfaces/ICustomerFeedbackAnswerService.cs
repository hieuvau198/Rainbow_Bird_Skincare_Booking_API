using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerFeedbackAnswerService
    {
        Task<IEnumerable<CustomerFeedbackAnswerDto>> GetAllAnswersAsync();
        Task<CustomerFeedbackAnswerDto> GetAnswerByIdAsync(int id);
        Task<IEnumerable<CustomerFeedbackAnswerDto>> GetAnswersByFeedbackIdAsync(int feedbackId);
        Task<CustomerFeedbackAnswerDto> CreateAnswerAsync(CreateCustomerFeedbackAnswerDto createDto);
        Task UpdateAnswerAsync(int id, UpdateCustomerFeedbackAnswerDto updateDto);
        Task DeleteAnswerAsync(int id);
        Task<bool> AnswerExistsAsync(int id);
    }
}