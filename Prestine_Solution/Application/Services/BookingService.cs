using Application.DTOs;
using Application.Interfaces;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> _repository;
        private readonly IMapper _mapper;

        public BookingService(
            IGenericRepository<Booking> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // ✅ Efficiently fetch all bookings with necessary relationships
        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _repository.GetAllAsync(null, b => b.Customer, b => b.Therapist);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        // ✅ Get booking by ID with necessary relationships
        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id, b => b.Customer, b => b.Therapist);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            return _mapper.Map<BookingDto>(booking);
        }

        // ✅ Optimized search for customer bookings
        public async Task<IEnumerable<BookingDto>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _repository.GetAllAsync(b => b.CustomerId == customerId, b => b.Customer);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings.OrderByDescending(b => b.BookingDate));
        }

        // ✅ Optimized search for therapist bookings
        public async Task<IEnumerable<BookingDto>> GetBookingsByTherapistIdAsync(int therapistId)
        {
            var bookings = await _repository.GetAllAsync(b => b.TherapistId == therapistId, b => b.Therapist);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings.OrderByDescending(b => b.BookingDate));
        }

        // ✅ Optimized search for service bookings
        public async Task<IEnumerable<BookingDto>> GetBookingsByServiceIdAsync(int serviceId)
        {
            var bookings = await _repository.GetAllAsync(b => b.ServiceId == serviceId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings.OrderByDescending(b => b.BookingDate));
        }

        // ✅ Get bookings by date efficiently
        public async Task<IEnumerable<BookingDto>> GetBookingsByDateAsync(DateOnly date)
        {
            var bookings = await _repository.GetAllAsync(b => b.BookingDate == date);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings.OrderBy(b => b.SlotId));
        }

        // ✅ Get booking status & next allowed transitions
        public async Task<GetBookingStatusDto> GetBookingStatusAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            if (!Enum.TryParse<BookingStatus>(booking.Status, out var currentStatus))
                throw new InvalidOperationException("The booking has an invalid status. Please contact support.");

            var nextStatuses = BookingStatusHelper.GetNextStatuses(currentStatus)
                                .Select(s => BookingStatusHelper.GetStatusDisplayName(s))
                                .ToList(); // ✅ Ensure it's a List<string>

            return new GetBookingStatusDto
            {
                CurrentStatus = BookingStatusHelper.GetStatusDisplayName(currentStatus),
                NextStatuses = nextStatuses // ✅ Now it's a List<string>
            };
        }


        // ✅ Prevent duplicate bookings & default to "Awaiting Confirmation"
        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createDto)
        {
            var existingBookings = await _repository.GetAllAsync(b =>
                b.CustomerId == createDto.CustomerId &&
                b.BookingDate == createDto.BookingDate &&
                b.SlotId == createDto.SlotId
            );

            if (existingBookings.Any())
                throw new InvalidOperationException("You already have a booking for this time slot. Please choose a different time.");

            var booking = _mapper.Map<Booking>(createDto);
            booking.CreatedAt = DateTime.UtcNow;
            booking.Status = BookingStatus.AwaitingConfirmation.ToString(); // ✅ No "Pending" status

            await _repository.CreateAsync(booking);
            return _mapper.Map<BookingDto>(booking);
        }

        // ✅ Update booking details
        public async Task UpdateBookingAsync(int id, UpdateBookingDto updateDto)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            _mapper.Map(updateDto, booking);
            await _repository.UpdateAsync(booking);
        }

        // ✅ Update booking status with validation
        public async Task UpdateBookingStatusAsync(int id, string newStatusString)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            if (!Enum.TryParse<BookingStatus>(newStatusString, true, out var newStatus))
                throw new ArgumentException($"The status '{newStatusString}' is not valid. Please try again.");

            if (!Enum.TryParse<BookingStatus>(booking.Status, out var currentStatus))
                throw new InvalidOperationException("The booking has an invalid status. Please contact support.");

            var allowedStatuses = BookingStatusHelper.GetNextStatuses(currentStatus);
            if (!allowedStatuses.Contains(newStatus))
                throw new InvalidOperationException($"You cannot change the status from '{currentStatus}' to '{newStatus}'. Please check and try again.");

            booking.Status = newStatus.ToString();
            await _repository.UpdateAsync(booking);
        }

        // ✅ Overloaded method for DTO-based status update
        public async Task UpdateBookingStatusAsync(int id, UpdateBookingDto updateDto)
        {
            if (updateDto.Status == null)
                throw new ArgumentException("Status cannot be null.");

            await UpdateBookingStatusAsync(id, updateDto.Status);
        }

        // ✅ Delete a booking
        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist.");

            await _repository.DeleteAsync(booking);
        }
    }
}
