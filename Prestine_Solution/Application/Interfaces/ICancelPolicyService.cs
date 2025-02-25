using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICancelPolicyService
    {
        Task<IEnumerable<CancelPolicyDto>> GetAllCancelPoliciesAsync();
        Task<CancelPolicyDto> GetCancelPolicyByIdAsync(int id);
        Task<IEnumerable<CancelPolicyDto>> GetActiveCancelPoliciesAsync();
        Task<CancelPolicyDto> CreateCancelPolicyAsync(CreateCancelPolicyDto createDto);
        Task UpdateCancelPolicyAsync(int id, UpdateCancelPolicyDto updateDto);
        Task DeleteCancelPolicyAsync(int id);
    }
}
