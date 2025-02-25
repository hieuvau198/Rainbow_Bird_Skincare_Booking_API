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
    public class CancelPolicyService : ICancelPolicyService
    {
        private readonly IGenericRepository<CancelPolicy> _repository;
        private readonly IMapper _mapper;

        public CancelPolicyService(
            IGenericRepository<CancelPolicy> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CancelPolicyDto>> GetAllCancelPoliciesAsync()
        {
            var cancelPolicies = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CancelPolicyDto>>(cancelPolicies);
        }

        public async Task<CancelPolicyDto> GetCancelPolicyByIdAsync(int id)
        {
            var cancelPolicy = await _repository.GetByIdAsync(id);
            if (cancelPolicy == null)
                throw new KeyNotFoundException($"Cancel policy with ID {id} not found");

            return _mapper.Map<CancelPolicyDto>(cancelPolicy);
        }

        public async Task<IEnumerable<CancelPolicyDto>> GetActiveCancelPoliciesAsync()
        {
            var cancelPolicies = await _repository.GetAllAsync();
            var activePolicies = cancelPolicies.Where(p => p.IsActive == true);
            return _mapper.Map<IEnumerable<CancelPolicyDto>>(activePolicies);
        }

        public async Task<CancelPolicyDto> CreateCancelPolicyAsync(CreateCancelPolicyDto createDto)
        {
            var cancelPolicy = _mapper.Map<CancelPolicy>(createDto);
            cancelPolicy.CreatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(cancelPolicy);
            return _mapper.Map<CancelPolicyDto>(cancelPolicy);
        }

        public async Task UpdateCancelPolicyAsync(int id, UpdateCancelPolicyDto updateDto)
        {
            var cancelPolicy = await _repository.GetByIdAsync(id);
            if (cancelPolicy == null)
                throw new KeyNotFoundException($"Cancel policy with ID {id} not found");

            _mapper.Map(updateDto, cancelPolicy);
            await _repository.UpdateAsync(cancelPolicy);
        }

        public async Task DeleteCancelPolicyAsync(int id)
        {
            var cancelPolicy = await _repository.GetByIdAsync(id);
            if (cancelPolicy == null)
                throw new KeyNotFoundException($"Cancel policy with ID {id} not found");

            await _repository.DeleteAsync(cancelPolicy);
        }
    }
}
