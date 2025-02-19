using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto> GetPaymentByIdAsync(int id);
        Task<IEnumerable<PaymentDto>> GetPaymentsByStatusAsync(string status);
        Task<IEnumerable<PaymentDto>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createDto);
        Task UpdatePaymentAsync(int id, UpdatePaymentDto updateDto);
        Task DeletePaymentAsync(int id);
    }
}
