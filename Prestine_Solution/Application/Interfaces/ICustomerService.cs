using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<CustomerDto> GetCustomerByUserIdAsync(int userId);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createDto);
        Task<CustomerDto> CreateCustomerWithUserAsync(CreateCustomerUserDto createDto);
        Task UpdateCustomerAsync(int id, UpdateCustomerDto updateDto);
        Task DeleteCustomerAsync(int id);
        Task<IEnumerable<CustomerDto>> GetCustomersByLastVisitDateAsync(DateTime date);
        Task UpdateLastVisitDateAsync(int id, DateTime lastVisitDate);
    }
}
