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
    public class CustomerQuizService : ICustomerQuizService
    {
        private readonly IGenericRepository<CustomerQuiz> _repository;
        private readonly IGenericRepository<Quiz> _quizRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerQuizService(
            IGenericRepository<CustomerQuiz> repository,
            IGenericRepository<Quiz> quizRepository,
            IGenericRepository<Customer> customerRepository,
            IMapper mapper)
        {
            _repository = repository;
            _quizRepository = quizRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerQuizDto>> GetAllCustomerQuizzesAsync()
        {
            var customerQuizzes = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerQuizDto>>(customerQuizzes);
        }

        public async Task<CustomerQuizDto> GetCustomerQuizByIdAsync(int id)
        {
            var customerQuiz = await _repository.GetByIdAsync(id);
            if (customerQuiz == null)
                throw new KeyNotFoundException($"Customer quiz with ID {id} not found");

            return _mapper.Map<CustomerQuizDto>(customerQuiz);
        }

        public async Task<IEnumerable<CustomerQuizDto>> GetCustomerQuizzesByCustomerIdAsync(int customerId)
        {
            var customerQuizzes = await _repository.GetAllAsync();
            var filtered = customerQuizzes.Where(cq => cq.CustomerId == customerId)
                                        .OrderByDescending(cq => cq.StartedAt);
            return _mapper.Map<IEnumerable<CustomerQuizDto>>(filtered);
        }

        public async Task<CustomerQuizDto> StartQuizAsync(CreateCustomerQuizDto createDto)
        {
            // Verify customer exists
            var customer = await _customerRepository.GetByIdAsync(createDto.CustomerId);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {createDto.CustomerId} not found");

            // Verify quiz exists
            var quiz = await _quizRepository.GetByIdAsync(createDto.QuizId);
            if (quiz == null)
                throw new KeyNotFoundException($"Quiz with ID {createDto.QuizId} not found");

            var customerQuiz = _mapper.Map<CustomerQuiz>(createDto);
            customerQuiz.StartedAt = DateTime.UtcNow;
            customerQuiz.Status = "In Progress";

            await _repository.CreateAsync(customerQuiz);
            return _mapper.Map<CustomerQuizDto>(customerQuiz);
        }

        public async Task UpdateCustomerQuizAsync(int id, UpdateCustomerQuizDto updateDto)
        {
            var customerQuiz = await _repository.GetByIdAsync(id);
            if (customerQuiz == null)
                throw new KeyNotFoundException($"Customer quiz with ID {id} not found");

            _mapper.Map(updateDto, customerQuiz);

            if (updateDto.Status?.ToLower() == "completed" && !customerQuiz.CompletedAt.HasValue)
            {
                customerQuiz.CompletedAt = DateTime.UtcNow;
            }

            await _repository.UpdateAsync(customerQuiz);
        }

        public async Task DeleteCustomerQuizAsync(int id)
        {
            var customerQuiz = await _repository.GetByIdAsync(id);
            if (customerQuiz == null)
                throw new KeyNotFoundException($"Customer quiz with ID {id} not found");

            await _repository.DeleteAsync(customerQuiz);
        }

        public async Task<IEnumerable<CustomerQuizDto>> GetCompletedQuizzesByCustomerIdAsync(int customerId)
        {
            var customerQuizzes = await _repository.GetAllAsync();
            var filtered = customerQuizzes.Where(cq => cq.CustomerId == customerId &&
                                                     cq.Status?.ToLower() == "completed")
                                        .OrderByDescending(cq => cq.CompletedAt);
            return _mapper.Map<IEnumerable<CustomerQuizDto>>(filtered);
        }
    }
}
