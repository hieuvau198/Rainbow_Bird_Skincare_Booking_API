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
using System.Text;
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

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException($"Booking with ID {id} not found");

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _repository.GetAllAsync();
            var filtered = bookings.Where(b => b.CustomerId == customerId)
                                  .OrderByDescending(b => b.BookingDate);
            return _mapper.Map<IEnumerable<BookingDto>>(filtered);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByTherapistIdAsync(int therapistId)
        {
            var bookings = await _repository.GetAllAsync();
            var filtered = bookings.Where(b => b.TherapistId == therapistId)
                                  .OrderByDescending(b => b.BookingDate);
            return _mapper.Map<IEnumerable<BookingDto>>(filtered);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByServiceIdAsync(int serviceId)
        {
            var bookings = await _repository.GetAllAsync();
            var filtered = bookings.Where(b => b.ServiceId == serviceId)
                                  .OrderByDescending(b => b.BookingDate);
            return _mapper.Map<IEnumerable<BookingDto>>(filtered);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByDateAsync(DateOnly date)
        {
            var bookings = await _repository.GetAllAsync();
            var filtered = bookings.Where(b => b.BookingDate == date)
                                  .OrderBy(b => b.SlotId);
            return _mapper.Map<IEnumerable<BookingDto>>(filtered);
        }

        // Get current status and next possible statuses
        public async Task<GetBookingStatusDto> GetBookingStatusAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist. Please check your details.");

            if (!Enum.TryParse<BookingStatus>(booking.Status, out var currentStatus))
                throw new InvalidOperationException("The booking has an invalid status. Please contact support.");

            var nextStatuses = BookingStatusHelper.GetNextStatuses(currentStatus);

            return new GetBookingStatusDto
            {
                CurrentStatus = currentStatus.ToString(),
                NextStatuses = nextStatuses.Select(s => s.ToString()).ToList()
            };
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createDto)
        {
            // Check if the customer already has a booking for the same slot & date
            var existingBookings = await _repository.GetAllAsync(b =>
                b.CustomerId == createDto.CustomerId &&
                b.BookingDate == createDto.BookingDate &&
                b.SlotId == createDto.SlotId);

            if (existingBookings.Any())
            {
                throw new InvalidOperationException("You already have a booking for this time slot. Please choose a different time.");
            }

            var booking = _mapper.Map<Booking>(createDto);
            booking.CreatedAt = DateTime.UtcNow;
            booking.Status = "Pending"; // Default status

            await _repository.CreateAsync(booking);
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task UpdateBookingAsync(int id, UpdateBookingDto updateDto)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException($"Booking with ID {id} not found");

            _mapper.Map(updateDto, booking);
            await _repository.UpdateAsync(booking);
        }



        // Update status (string or DTO)
        public async Task UpdateBookingStatusAsync(int id, string newStatusString)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("The requested booking does not exist. Please check your details.");

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

        // Overloaded version: Accepts UpdateBookingDto
        public async Task UpdateBookingStatusAsync(int id, UpdateBookingDto updateDto)
        {
            if (updateDto.Status == null)
                throw new ArgumentException("Status cannot be null");

            await UpdateBookingStatusAsync(id, updateDto.Status);
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException($"Booking with ID {id} not found");

            await _repository.DeleteAsync(booking);
        }
    }
}
