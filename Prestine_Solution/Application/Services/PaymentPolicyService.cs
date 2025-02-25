using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentPolicyService : IPaymentPolicyService
    {
        private readonly IGenericRepository<PaymentPolicy> _repository;
        private readonly IMapper _mapper;

        public PaymentPolicyService(
            IGenericRepository<PaymentPolicy> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentPolicyDto>> GetAllPaymentPoliciesAsync()
        {
            var paymentPolicies = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentPolicyDto>>(paymentPolicies);
        }

        public async Task<PaymentPolicyDto> GetPaymentPolicyByIdAsync(int id)
        {
            var paymentPolicy = await _repository.GetByIdAsync(id);
            if (paymentPolicy == null)
                throw new KeyNotFoundException($"Payment policy with ID {id} not found");

            return _mapper.Map<PaymentPolicyDto>(paymentPolicy);
        }

        public async Task<IEnumerable<PaymentPolicyDto>> GetActivePaymentPoliciesAsync()
        {
            var paymentPolicies = await _repository.GetAllAsync();
            var activePolicies = paymentPolicies.Where(p => p.IsActive == true);
            return _mapper.Map<IEnumerable<PaymentPolicyDto>>(activePolicies);
        }

        public async Task<PaymentPolicyDto> CreatePaymentPolicyAsync(CreatePaymentPolicyDto createDto)
        {
            var paymentPolicy = _mapper.Map<PaymentPolicy>(createDto);
            paymentPolicy.CreatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(paymentPolicy);
            return _mapper.Map<PaymentPolicyDto>(paymentPolicy);
        }

        public async Task UpdatePaymentPolicyAsync(int id, UpdatePaymentPolicyDto updateDto)
        {
            var paymentPolicy = await _repository.GetByIdAsync(id);
            if (paymentPolicy == null)
                throw new KeyNotFoundException($"Payment policy with ID {id} not found");

            _mapper.Map(updateDto, paymentPolicy);
            await _repository.UpdateAsync(paymentPolicy);
        }

        public async Task DeletePaymentPolicyAsync(int id)
        {
            var paymentPolicy = await _repository.GetByIdAsync(id);
            if (paymentPolicy == null)
                throw new KeyNotFoundException($"Payment policy with ID {id} not found");

            await _repository.DeleteAsync(paymentPolicy);
        }
    }
}
