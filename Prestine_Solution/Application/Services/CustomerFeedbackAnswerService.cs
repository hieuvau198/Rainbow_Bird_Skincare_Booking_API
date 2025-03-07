using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Services
{
    public class CustomerFeedbackAnswerService : ICustomerFeedbackAnswerService
    {
        private readonly IGenericRepository<CustomerFeedbackAnswer> _repository;
        private readonly IMapper _mapper;

        public CustomerFeedbackAnswerService(
            IGenericRepository<CustomerFeedbackAnswer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerFeedbackAnswerDto>> GetAllAnswersAsync()
        {
            var answers = await _repository.GetAllAsync(
                null,
                a => a.CustomerFeedback
            );
            return _mapper.Map<IEnumerable<CustomerFeedbackAnswerDto>>(answers);
        }

        public async Task<CustomerFeedbackAnswerDto> GetAnswerByIdAsync(int id)
        {
            var answer = await _repository.GetByIdAsync(
                id,
                a => a.CustomerFeedback
            );

            if (answer == null)
                throw new KeyNotFoundException($"Customer feedback answer with ID {id} not found");

            return _mapper.Map<CustomerFeedbackAnswerDto>(answer);
        }

        public async Task<IEnumerable<CustomerFeedbackAnswerDto>> GetAnswersByFeedbackIdAsync(int feedbackId)
        {
            var answers = await _repository.GetAllAsync(
                a => a.CustomerFeedbackId == feedbackId,
                a => a.CustomerFeedback
            );

            return _mapper.Map<IEnumerable<CustomerFeedbackAnswerDto>>(answers);
        }

        public async Task<CustomerFeedbackAnswerDto> CreateAnswerAsync(CreateCustomerFeedbackAnswerDto createDto)
        {
            var answer = _mapper.Map<CustomerFeedbackAnswer>(createDto);
            answer.CreatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(answer);
            return _mapper.Map<CustomerFeedbackAnswerDto>(answer);
        }

        public async Task UpdateAnswerAsync(int id, UpdateCustomerFeedbackAnswerDto updateDto)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"Customer feedback answer with ID {id} not found");

            _mapper.Map(updateDto, answer);

            await _repository.UpdateAsync(answer);
        }

        public async Task DeleteAnswerAsync(int id)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"Customer feedback answer with ID {id} not found");

            await _repository.DeleteAsync(answer);
        }

        public async Task<bool> AnswerExistsAsync(int id)
        {
            return await _repository.ExistsAsync(a => a.ResponseId == id);
        }
    }
}