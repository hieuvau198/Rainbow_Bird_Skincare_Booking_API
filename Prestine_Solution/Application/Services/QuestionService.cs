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
    public class QuestionService : IQuestionService
    {
        private readonly IGenericRepository<Question> _repository;
        private readonly IGenericRepository<Quiz> _quizRepository;
        private readonly IMapper _mapper;

        public QuestionService(
            IGenericRepository<Question> repository,
            IGenericRepository<Quiz> quizRepository,
            IMapper mapper)
        {
            _repository = repository;
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync()
        {
            var questions = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(int id)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found");

            return _mapper.Map<QuestionDto>(question);
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId)
        {
            var questions = await _repository.GetAllAsync();
            var filtered = questions.Where(q => q.QuizId == quizId)
                                  .OrderBy(q => q.DisplayOrder);
            return _mapper.Map<IEnumerable<QuestionDto>>(filtered);
        }

        public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto createDto)
        {
            var quiz = await _quizRepository.GetByIdAsync(createDto.QuizId);
            if (quiz == null)
                throw new KeyNotFoundException($"Quiz with ID {createDto.QuizId} not found");

            var question = _mapper.Map<Question>(createDto);
            question.CreatedAt = DateTime.UtcNow;
            question.IsActive = true;

            await _repository.CreateAsync(question);
            return _mapper.Map<QuestionDto>(question);
        }

        public async Task UpdateQuestionAsync(int id, UpdateQuestionDto updateDto)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found");

            _mapper.Map(updateDto, question);
            await _repository.UpdateAsync(question);
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found");

            await _repository.DeleteAsync(question);
        }

        public async Task<IEnumerable<QuestionDto>> GetActiveQuestionsByQuizIdAsync(int quizId)
        {
            var questions = await _repository.GetAllAsync();
            var filtered = questions.Where(q => q.QuizId == quizId && q.IsActive == true)
                                  .OrderBy(q => q.DisplayOrder);
            return _mapper.Map<IEnumerable<QuestionDto>>(filtered);
        }
    }
}
