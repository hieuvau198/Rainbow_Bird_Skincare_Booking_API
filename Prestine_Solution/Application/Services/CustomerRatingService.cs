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
    public class CustomerRatingService : ICustomerRatingService
    {
        private readonly IGenericRepository<CustomerRating> _repository;
        private readonly IMapper _mapper;

        public CustomerRatingService(
            IGenericRepository<CustomerRating> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerRatingDto>> GetAllRatingsAsync()
        {
            var ratings = await _repository.GetAllAsync(null, r => r.Booking);
            return _mapper.Map<IEnumerable<CustomerRatingDto>>(ratings);
        }

        public async Task<CustomerRatingDto> GetRatingByIdAsync(int id)
        {
            var rating = await _repository.GetByIdAsync(id, r => r.Booking);

            if (rating == null)
                throw new KeyNotFoundException($"Rating with ID {id} not found");

            return _mapper.Map<CustomerRatingDto>(rating);
        }

        public async Task<IEnumerable<CustomerRatingDto>> GetRatingsByBookingIdAsync(int bookingId)
        {
            var ratings = await _repository.GetAllAsync(
                r => r.BookingId == bookingId,
                r => r.Booking
            );

            return _mapper.Map<IEnumerable<CustomerRatingDto>>(ratings);
        }

        public async Task<CustomerRatingDto> CreateRatingAsync(CreateCustomerRatingDto createDto)
        {
            if (createDto.RatingValue < 1 || createDto.RatingValue > 5)
                throw new InvalidOperationException("Rating value must be between 1 and 5");

            var rating = _mapper.Map<CustomerRating>(createDto);
            rating.CreatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(rating);
            return _mapper.Map<CustomerRatingDto>(rating);
        }

        public async Task UpdateRatingAsync(int id, UpdateCustomerRatingDto updateDto)
        {
            var rating = await _repository.GetByIdAsync(id);
            if (rating == null)
                throw new KeyNotFoundException($"Rating with ID {id} not found");

            if (updateDto.RatingValue.HasValue && (updateDto.RatingValue < 1 || updateDto.RatingValue > 5))
                throw new InvalidOperationException("Rating value must be between 1 and 5");

            _mapper.Map(updateDto, rating);

            await _repository.UpdateAsync(rating);
        }

        public async Task DeleteRatingAsync(int id)
        {
            var rating = await _repository.GetByIdAsync(id);
            if (rating == null)
                throw new KeyNotFoundException($"Rating with ID {id} not found");

            await _repository.DeleteAsync(rating);
        }

        public async Task<bool> RatingExistsAsync(int id)
        {
            return await _repository.ExistsAsync(r => r.RatingId == id);
        }

        public async Task<bool> HasUserRatedBookingAsync(int bookingId)
        {
            return await _repository.ExistsAsync(r => r.BookingId == bookingId);
        }
    }
}