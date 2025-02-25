using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentPolicyService
    {
        Task<IEnumerable<PaymentPolicyDto>> GetAllPaymentPoliciesAsync();
        Task<PaymentPolicyDto> GetPaymentPolicyByIdAsync(int id);
        Task<IEnumerable<PaymentPolicyDto>> GetActivePaymentPoliciesAsync();
        Task<PaymentPolicyDto> CreatePaymentPolicyAsync(CreatePaymentPolicyDto createDto);
        Task UpdatePaymentPolicyAsync(int id, UpdatePaymentPolicyDto updateDto);
        Task DeletePaymentPolicyAsync(int id);
    }
}
