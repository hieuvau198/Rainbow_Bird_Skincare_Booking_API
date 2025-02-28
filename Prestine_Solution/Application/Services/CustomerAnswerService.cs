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
    public class CustomerAnswerService : ICustomerAnswerService
    {
        private readonly IGenericRepository<CustomerAnswer> _repository;
        private readonly IMapper _mapper;

        public CustomerAnswerService(
            IGenericRepository<CustomerAnswer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerAnswerDto>> GetAllCustomerAnswersAsync()
        {
            // ✅ Corrected `GetAllAsync` usage (includes must be separate from predicate)
            var answers = await _repository.GetAllAsync(null,
                t => t.Answer,
                t => t.CustomerQuiz,
                t => t.Answer.Question);

            return _mapper.Map<IEnumerable<CustomerAnswerDto>>(answers);
        }

        public async Task<CustomerAnswerDto> GetCustomerAnswerByIdAsync(int id)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"CustomerAnswer with ID {id} not found");

            return _mapper.Map<CustomerAnswerDto>(answer);
        }

        public async Task<IEnumerable<CustomerAnswerDto>> GetAnswersByCustomerQuizIdAsync(int customerQuizId)
        {
            // ✅ Optimized: Filtering now happens at the database level
            var answers = await _repository.GetAllAsync(a => a.CustomerQuizId == customerQuizId);
            return _mapper.Map<IEnumerable<CustomerAnswerDto>>(answers.OrderBy(a => a.AnsweredAt));
        }

        public async Task<IEnumerable<CustomerAnswerDto>> GetAnswersByQuestionIdAsync(int questionId)
        {
            // ✅ Optimized: Filtering now happens at the database level
            var answers = await _repository.GetAllAsync(a => a.QuestionId == questionId);
            return _mapper.Map<IEnumerable<CustomerAnswerDto>>(answers.OrderBy(a => a.AnsweredAt));
        }


        public async Task<CustomerAnswerDto> CreateCustomerAnswerAsync(CreateCustomerAnswerDto createDto)
        {
            var answer = _mapper.Map<CustomerAnswer>(createDto);
            answer.AnsweredAt = DateTime.UtcNow;

            await _repository.CreateAsync(answer);
            return _mapper.Map<CustomerAnswerDto>(answer);
        }

        public async Task UpdateCustomerAnswerAsync(int id, UpdateCustomerAnswerDto updateDto)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"CustomerAnswer with ID {id} not found");

            _mapper.Map(updateDto, answer);
            await _repository.UpdateAsync(answer);
        }

        public async Task DeleteCustomerAnswerAsync(int id)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"CustomerAnswer with ID {id} not found");

            await _repository.DeleteAsync(answer);
        }
    }
}
