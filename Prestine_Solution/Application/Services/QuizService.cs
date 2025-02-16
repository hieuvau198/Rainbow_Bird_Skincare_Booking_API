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
    public class QuizService : IQuizService
    {
        private readonly IGenericRepository<Quiz> _repository;
        private readonly IMapper _mapper;

        public QuizService(
            IGenericRepository<Quiz> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzesAsync()
        {
            var quizzes = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<QuizDto>>(quizzes);
        }

        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            var quiz = await _repository.GetByIdAsync(id);
            if (quiz == null)
                throw new KeyNotFoundException($"Quiz with ID {id} not found");

            return _mapper.Map<QuizDto>(quiz);
        }

        public async Task<QuizDto> CreateQuizAsync(CreateQuizDto createDto)
        {
            var quiz = _mapper.Map<Quiz>(createDto);
            quiz.CreatedAt = DateTime.UtcNow;
            quiz.IsActive = true;

            await _repository.CreateAsync(quiz);
            return _mapper.Map<QuizDto>(quiz);
        }

        public async Task UpdateQuizAsync(int id, UpdateQuizDto updateDto)
        {
            var quiz = await _repository.GetByIdAsync(id);
            if (quiz == null)
                throw new KeyNotFoundException($"Quiz with ID {id} not found");

            _mapper.Map(updateDto, quiz);
            await _repository.UpdateAsync(quiz);
        }

        public async Task DeleteQuizAsync(int id)
        {
            var quiz = await _repository.GetByIdAsync(id);
            if (quiz == null)
                throw new KeyNotFoundException($"Quiz with ID {id} not found");

            await _repository.DeleteAsync(quiz);
        }

        public async Task<IEnumerable<QuizDto>> GetActiveQuizzesAsync()
        {
            var quizzes = await _repository.GetAllAsync();
            var activeQuizzes = quizzes.Where(q => q.IsActive == true);
            return _mapper.Map<IEnumerable<QuizDto>>(activeQuizzes);
        }

        public async Task<IEnumerable<QuizDto>> GetQuizzesByCategoryAsync(string category)
        {
            var quizzes = await _repository.GetAllAsync();
            var filteredQuizzes = quizzes.Where(q => q.Category == category);
            return _mapper.Map<IEnumerable<QuizDto>>(filteredQuizzes);
        }
    }
}
