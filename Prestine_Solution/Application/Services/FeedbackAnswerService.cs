using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Services
{
    public class FeedbackAnswerService : IFeedbackAnswerService
    {
        private readonly IGenericRepository<FeedbackAnswer> _repository;
        private readonly IMapper _mapper;

        public FeedbackAnswerService(
            IGenericRepository<FeedbackAnswer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeedbackAnswerDto>> GetAllAnswersAsync()
        {
            var answers = await _repository.GetAllAsync(null, a => a.FeedbackQuestion);
            return _mapper.Map<IEnumerable<FeedbackAnswerDto>>(answers);
        }

        public async Task<FeedbackAnswerDto> GetAnswerByIdAsync(int id)
        {
            var answer = await _repository.GetByIdAsync(id, a => a.FeedbackQuestion);

            if (answer == null)
                throw new KeyNotFoundException($"Feedback answer with ID {id} not found");

            return _mapper.Map<FeedbackAnswerDto>(answer);
        }

        public async Task<IEnumerable<FeedbackAnswerDto>> GetAnswersByQuestionIdAsync(int questionId)
        {
            var answers = await _repository.GetAllAsync(
                a => a.FeedbackQuestionId == questionId,
                a => a.FeedbackQuestion
            );

            return _mapper.Map<IEnumerable<FeedbackAnswerDto>>(answers);
        }

        public async Task<FeedbackAnswerDto> CreateAnswerAsync(CreateFeedbackAnswerDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.AnswerText))
                throw new InvalidOperationException("Answer text cannot be empty");

            var answer = _mapper.Map<FeedbackAnswer>(createDto);
            answer.CreatedAt = DateTime.UtcNow;
            answer.UpdatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(answer);
            return _mapper.Map<FeedbackAnswerDto>(answer);
        }

        public async Task UpdateAnswerAsync(int id, UpdateFeedbackAnswerDto updateDto)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"Feedback answer with ID {id} not found");

            if (updateDto.AnswerText != null && string.IsNullOrWhiteSpace(updateDto.AnswerText))
                throw new InvalidOperationException("Answer text cannot be empty");

            _mapper.Map(updateDto, answer);
            answer.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(answer);
        }

        public async Task DeleteAnswerAsync(int id)
        {
            var answer = await _repository.GetByIdAsync(id);
            if (answer == null)
                throw new KeyNotFoundException($"Feedback answer with ID {id} not found");

            await _repository.DeleteAsync(answer);
        }

        public async Task<bool> AnswerExistsAsync(int id)
        {
            return await _repository.ExistsAsync(a => a.AnswerOptionId == id);
        }

        public async Task<int> CountAnswersByQuestionIdAsync(int questionId)
        {
            var answers = await _repository.GetAllAsync(a => a.FeedbackQuestionId == questionId);
            return answers.Count();
        }
    }
}