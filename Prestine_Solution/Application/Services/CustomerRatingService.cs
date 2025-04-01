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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerRatingService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerRatingDto>> GetAllRatingsAsync()
        {
            var ratings = await _unitOfWork.CustomerRatings.GetAllAsync(null, r => r.Booking);
            return _mapper.Map<IEnumerable<CustomerRatingDto>>(ratings);
        }

        public async Task<CustomerRatingDto> GetRatingByIdAsync(int id)
        {
            var rating = await _unitOfWork.CustomerRatings.GetByIdAsync(id, r => r.Booking);

            if (rating == null)
                throw new KeyNotFoundException($"Rating with ID {id} not found");

            return _mapper.Map<CustomerRatingDto>(rating);
        }

        public async Task<IEnumerable<CustomerRatingDto>> GetRatingsByBookingIdAsync(int bookingId)
        {
            var ratings = await _unitOfWork.CustomerRatings.GetAllAsync(
                r => r.BookingId == bookingId,
                r => r.Booking
            );

            return _mapper.Map<IEnumerable<CustomerRatingDto>>(ratings);
        }

        public async Task<IEnumerable<CustomerRatingDto>> GetRatingsByServiceIdAsync(int serviceId)
        {
            var ratings = await _unitOfWork.CustomerRatings.GetAllAsync(
                r => r.Booking.ServiceId == serviceId,
                r => r.Booking
            );

            return _mapper.Map<IEnumerable<CustomerRatingDto>>(ratings);
        }

        public async Task<CustomerRatingDto> CreateRatingAsync(CreateCustomerRatingDto createDto)
        {
            if (createDto.RatingValue < 1 || createDto.RatingValue > 5)
                throw new InvalidOperationException("Rating value must be between 1 and 5");

            var booking = await _unitOfWork.Bookings.GetByIdAsync(createDto.BookingId);

            if (booking == null)
                throw new InvalidOperationException("This booking is not existed");

            var existedRating = await _unitOfWork.CustomerRatings.FindAsync(r => r.BookingId == createDto.BookingId);
            if (existedRating != null)
                throw new InvalidOperationException("You rated this service booking already.");

            try
            {
                if (booking != null)
                {
                    booking.IsRated = true;
                    var service = await _unitOfWork.Services.GetByIdAsync(booking.ServiceId);
                    var therapist = await _unitOfWork.Therapists.GetByIdAsync((int)booking.TherapistId);

                    service.Rating = (service.Rating * service.RatingCount + createDto.RatingValue) / (service.RatingCount + 1);
                    service.RatingCount++;

                    therapist.Rating = (therapist.Rating * therapist.RatingCount + createDto.RatingValue) / (therapist.RatingCount + 1);
                    therapist.RatingCount++;

                    await _unitOfWork.Bookings.UpdateAsync(booking);
                    await _unitOfWork.Services.UpdateAsync(service);
                    await _unitOfWork.Therapists.UpdateAsync(therapist);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            var rating = _mapper.Map<CustomerRating>(createDto);
            rating.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.CustomerRatings.CreateAsync(rating);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CustomerRatingDto>(rating);
        }

        public async Task UpdateRatingAsync(int id, UpdateCustomerRatingDto updateDto)
        {
            var rating = await _unitOfWork.CustomerRatings.GetByIdAsync(id);
            if (rating == null)
                throw new KeyNotFoundException($"Rating with ID {id} not found");

            if (updateDto.RatingValue.HasValue && (updateDto.RatingValue < 1 || updateDto.RatingValue > 5))
                throw new InvalidOperationException("Rating value must be between 1 and 5");

            _mapper.Map(updateDto, rating);

            await _unitOfWork.CustomerRatings.UpdateAsync(rating);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRatingAsync(int id)
        {
            var rating = await _unitOfWork.CustomerRatings.GetByIdAsync(id);
            if (rating == null)
                throw new KeyNotFoundException($"Rating with ID {id} not found");

            await _unitOfWork.CustomerRatings.DeleteAsync(rating);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> RatingExistsAsync(int id)
        {
            return await _unitOfWork.CustomerRatings.ExistsAsync(r => r.RatingId == id);
        }

        public async Task<bool> HasUserRatedBookingAsync(int bookingId)
        {
            return await _unitOfWork.CustomerRatings.ExistsAsync(r => r.BookingId == bookingId);
        }
    }
}