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
    public class AnswerService : IAnswerService
    {
        private readonly IGenericRepository<Answer> _repository;
        private readonly IMapper _mapper;

        public AnswerService(
            IGenericRepository<Answer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnswerDto>> GetAllAnswersAsync()
        {
            var answers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AnswerDto>>(answers);
        }

        public async Task<AnswerDto> GetAnswerByIdAsync(int id)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found");

            return _mapper.Map<AnswerDto>(answer);
        }

        public async Task<IEnumerable<AnswerDto>> GetAnswersByQuestionIdAsync(int questionId)
        {
            var answers = await _repository.GetAllAsync();
            var questionAnswers = answers.Where(a => a.QuestionId == questionId);
            return _mapper.Map<IEnumerable<AnswerDto>>(questionAnswers);
        }

        public async Task<AnswerDto> CreateAnswerAsync(CreateAnswerDto createDto)
        {
            var answer = _mapper.Map<Answer>(createDto);
            answer.CreatedAt = DateTime.UtcNow;
            answer.IsActive = true;

            await _repository.CreateAsync(answer);
            return _mapper.Map<AnswerDto>(answer);
        }

        public async Task UpdateAnswerAsync(int id, UpdateAnswerDto updateDto)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found");

            _mapper.Map(updateDto, answer);
            await _repository.UpdateAsync(answer);
        }

        public async Task DeleteAnswerAsync(int id)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found");

            await _repository.DeleteAsync(answer);
        }
    }
}
