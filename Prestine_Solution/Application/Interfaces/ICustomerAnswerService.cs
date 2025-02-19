using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerAnswerService
    {
        Task<IEnumerable<CustomerAnswerDto>> GetAllCustomerAnswersAsync();
        Task<CustomerAnswerDto> GetCustomerAnswerByIdAsync(int id);
        Task<IEnumerable<CustomerAnswerDto>> GetAnswersByCustomerQuizIdAsync(int customerQuizId);
        Task<IEnumerable<CustomerAnswerDto>> GetAnswersByQuestionIdAsync(int questionId);
        Task<CustomerAnswerDto> CreateCustomerAnswerAsync(CreateCustomerAnswerDto createDto);
        Task UpdateCustomerAnswerAsync(int id, UpdateCustomerAnswerDto updateDto);
        Task DeleteCustomerAnswerAsync(int id);
    }
}
