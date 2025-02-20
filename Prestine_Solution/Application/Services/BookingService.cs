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

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createDto)
        {
            var booking = _mapper.Map<Booking>(createDto);
            booking.CreatedAt = DateTime.UtcNow;
            booking.Status = booking.Status ?? "Pending"; // Default status if not provided

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

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException($"Booking with ID {id} not found");

            await _repository.DeleteAsync(booking);
        }
    }
}
