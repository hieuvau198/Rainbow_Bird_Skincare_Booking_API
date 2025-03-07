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
    public class FeedbackQuestionService : IFeedbackQuestionService
    {
        private readonly IGenericRepository<FeedbackQuestion> _repository;
        private readonly IMapper _mapper;

        public FeedbackQuestionService(
            IGenericRepository<FeedbackQuestion> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeedbackQuestionDto>> GetAllQuestionsAsync()
        {
            var questions = await _repository.GetAllAsync(null, q => q.FeedbackAnswers);
            return _mapper.Map<IEnumerable<FeedbackQuestionDto>>(questions);
        }

        public async Task<FeedbackQuestionDto> GetQuestionByIdAsync(int id)
        {
            var question = await _repository.GetByIdAsync(id, q => q.FeedbackAnswers);

            if (question == null)
                throw new KeyNotFoundException($"Feedback question with ID {id} not found");

            return _mapper.Map<FeedbackQuestionDto>(question);
        }

        public async Task<FeedbackQuestionDto> CreateQuestionAsync(CreateFeedbackQuestionDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.QuestionText))
                throw new InvalidOperationException("Question text cannot be empty");

            var question = _mapper.Map<FeedbackQuestion>(createDto);
            question.CreatedAt = DateTime.UtcNow;
            question.UpdatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(question);
            return _mapper.Map<FeedbackQuestionDto>(question);
        }

        public async Task UpdateQuestionAsync(int id, UpdateFeedbackQuestionDto updateDto)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null)
                throw new KeyNotFoundException($"Feedback question with ID {id} not found");

            if (!string.IsNullOrWhiteSpace(updateDto.QuestionText))
            {
                _mapper.Map(updateDto, question);
                question.UpdatedAt = DateTime.UtcNow;
                await _repository.UpdateAsync(question);
            }
            else
            {
                throw new InvalidOperationException("Question text cannot be empty");
            }
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null)
                throw new KeyNotFoundException($"Feedback question with ID {id} not found");

            await _repository.DeleteAsync(question);
        }

        public async Task<bool> QuestionExistsAsync(int id)
        {
            return await _repository.ExistsAsync(q => q.QuestionId == id);
        }

        public async Task<IEnumerable<FeedbackQuestionDto>> SearchQuestionsAsync(string searchTerm)
        {
            var questions = await _repository.GetAllAsync(
                q => q.QuestionText.Contains(searchTerm),
                q => q.FeedbackAnswers
            );

            return _mapper.Map<IEnumerable<FeedbackQuestionDto>>(questions);
        }
    }
}