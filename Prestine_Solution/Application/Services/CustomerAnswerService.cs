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
            var answers = await _repository.GetAllAsync();
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
            var answers = await _repository.GetAllAsync();
            var filtered = answers.Where(a => a.CustomerQuizId == customerQuizId)
                                .OrderBy(a => a.AnsweredAt);
            return _mapper.Map<IEnumerable<CustomerAnswerDto>>(filtered);
        }

        public async Task<IEnumerable<CustomerAnswerDto>> GetAnswersByQuestionIdAsync(int questionId)
        {
            var answers = await _repository.GetAllAsync();
            var filtered = answers.Where(a => a.QuestionId == questionId)
                                .OrderBy(a => a.AnsweredAt);
            return _mapper.Map<IEnumerable<CustomerAnswerDto>>(filtered);
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
