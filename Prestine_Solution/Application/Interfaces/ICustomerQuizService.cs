using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerQuizService
    {
        Task<IEnumerable<CustomerQuizDto>> GetAllCustomerQuizzesAsync();
        Task<CustomerQuizDto> GetCustomerQuizByIdAsync(int id);
        Task<IEnumerable<CustomerQuizDto>> GetCustomerQuizzesByCustomerIdAsync(int customerId);
        Task<CustomerQuizDto> StartQuizAsync(CreateCustomerQuizDto createDto);
        Task UpdateCustomerQuizAsync(int id, UpdateCustomerQuizDto updateDto);
        Task DeleteCustomerQuizAsync(int id);
        Task<IEnumerable<CustomerQuizDto>> GetCompletedQuizzesByCustomerIdAsync(int customerId);
    }
}
