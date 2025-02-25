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
    public class CancelBookingService : ICancelBookingService
    {
        private readonly IGenericRepository<CancelBooking> _repository;
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly IMapper _mapper;

        public CancelBookingService(
            IGenericRepository<CancelBooking> repository,
            IGenericRepository<Booking> bookingRepository,
            IMapper mapper)
        {
            _repository = repository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CancelBookingDto>> GetAllCancelBookingsAsync()
        {
            var cancelBookings = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CancelBookingDto>>(cancelBookings);
        }

        public async Task<CancelBookingDto> GetCancelBookingByIdAsync(int id)
        {
            var cancelBooking = await _repository.GetByIdAsync(id);
            if (cancelBooking == null)
                throw new KeyNotFoundException($"Cancellation with ID {id} not found");

            return _mapper.Map<CancelBookingDto>(cancelBooking);
        }

        public async Task<CancelBookingDto> GetCancelBookingByBookingIdAsync(int bookingId)
        {
            var cancelBookings = await _repository.GetAllAsync();
            var cancelBooking = cancelBookings.FirstOrDefault(cb => cb.BookingId == bookingId);

            if (cancelBooking == null)
                throw new KeyNotFoundException($"Cancellation for booking ID {bookingId} not found");

            return _mapper.Map<CancelBookingDto>(cancelBooking);
        }

        public async Task<CancelBookingDto> CreateCancelBookingAsync(CreateCancelBookingDto createDto)
        {
            // Verify booking exists
            var booking = await _bookingRepository.GetByIdAsync(createDto.BookingId);
            if (booking == null)
                throw new KeyNotFoundException($"Booking with ID {createDto.BookingId} not found");

            // Check if cancellation already exists for this booking
            var existingCancellations = await _repository.GetAllAsync();
            if (existingCancellations.Any(c => c.BookingId == createDto.BookingId))
                throw new InvalidOperationException($"Booking with ID {createDto.BookingId} is already cancelled");

            var cancelBooking = _mapper.Map<CancelBooking>(createDto);
            cancelBooking.CancelledAt = DateTime.UtcNow;

            await _repository.CreateAsync(cancelBooking);
            return _mapper.Map<CancelBookingDto>(cancelBooking);
        }

        public async Task UpdateCancelBookingAsync(int id, UpdateCancelBookingDto updateDto)
        {
            var cancelBooking = await _repository.GetByIdAsync(id);
            if (cancelBooking == null)
                throw new KeyNotFoundException($"Cancellation with ID {id} not found");

            _mapper.Map(updateDto, cancelBooking);
            await _repository.UpdateAsync(cancelBooking);
        }

        public async Task DeleteCancelBookingAsync(int id)
        {
            var cancelBooking = await _repository.GetByIdAsync(id);
            if (cancelBooking == null)
                throw new KeyNotFoundException($"Cancellation with ID {id} not found");

            await _repository.DeleteAsync(cancelBooking);
        }
    }
}
