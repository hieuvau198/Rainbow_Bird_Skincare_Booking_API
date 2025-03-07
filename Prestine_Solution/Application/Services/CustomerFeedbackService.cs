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
    public class CustomerFeedbackService : ICustomerFeedbackService
    {
        private readonly IGenericRepository<CustomerFeedback> _repository;
        private readonly IMapper _mapper;

        public CustomerFeedbackService(
            IGenericRepository<CustomerFeedback> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerFeedbackDto>> GetAllFeedbacksAsync()
        {
            var feedbacks = await _repository.GetAllAsync(
                null,
                f => f.Booking,
                f => f.CustomerFeedbackAnswers
            );
            return _mapper.Map<IEnumerable<CustomerFeedbackDto>>(feedbacks);
        }

        public async Task<CustomerFeedbackDto> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _repository.GetByIdAsync(
                id,
                f => f.Booking,
                f => f.CustomerFeedbackAnswers
            );

            if (feedback == null)
                throw new KeyNotFoundException($"Customer feedback with ID {id} not found");

            return _mapper.Map<CustomerFeedbackDto>(feedback);
        }

        public async Task<IEnumerable<CustomerFeedbackDto>> GetFeedbacksByBookingIdAsync(int bookingId)
        {
            var feedbacks = await _repository.GetAllAsync(
                f => f.BookingId == bookingId,
                f => f.Booking,
                f => f.CustomerFeedbackAnswers
            );

            return _mapper.Map<IEnumerable<CustomerFeedbackDto>>(feedbacks);
        }

        public async Task<CustomerFeedbackDto> CreateFeedbackAsync(CreateCustomerFeedbackDto createDto)
        {
            var feedback = _mapper.Map<CustomerFeedback>(createDto);
            feedback.SubmittedAt = DateTime.UtcNow;

            await _repository.CreateAsync(feedback);
            return _mapper.Map<CustomerFeedbackDto>(feedback);
        }

        public async Task UpdateFeedbackAsync(int id, UpdateCustomerFeedbackDto updateDto)
        {
            var feedback = await _repository.GetByIdAsync(id);
            if (feedback == null)
                throw new KeyNotFoundException($"Customer feedback with ID {id} not found");

            _mapper.Map(updateDto, feedback);

            await _repository.UpdateAsync(feedback);
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _repository.GetByIdAsync(id);
            if (feedback == null)
                throw new KeyNotFoundException($"Customer feedback with ID {id} not found");

            await _repository.DeleteAsync(feedback);
        }

        public async Task<bool> FeedbackExistsAsync(int id)
        {
            return await _repository.ExistsAsync(f => f.CustomerFeedbackId == id);
        }

        public async Task<bool> HasBookingFeedbackAsync(int bookingId)
        {
            return await _repository.ExistsAsync(f => f.BookingId == bookingId);
        }

        public async Task<IEnumerable<CustomerFeedbackDto>> GetRecentFeedbacksAsync(int count)
        {
            // Get all feedbacks with includes
            var allFeedbacks = await _repository.GetAllAsync(
                null,
                f => f.Booking,
                f => f.CustomerFeedbackAnswers
            );

            // Order and take in memory
            var recentFeedbacks = allFeedbacks
                .OrderByDescending(f => f.SubmittedAt)
                .Take(count);

            return _mapper.Map<IEnumerable<CustomerFeedbackDto>>(recentFeedbacks);
        }
    }
}