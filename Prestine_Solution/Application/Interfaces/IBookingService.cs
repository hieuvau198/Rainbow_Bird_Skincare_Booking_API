using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingDto>> GetBookingsByCustomerIdAsync(int customerId);
        Task<IEnumerable<BookingDto>> GetBookingsByTherapistIdAsync(int therapistId);
        Task<IEnumerable<BookingDto>> GetBookingsByServiceIdAsync(int serviceId);
        Task<IEnumerable<BookingDto>> GetBookingsByDateAsync(DateOnly date);
        Task<GetBookingStatusDto> GetBookingStatusAsync(int id);
        Task<BookingDto> CreateBookingAsync(CreateBookingDto createDto);
        Task UpdateBookingAsync(int id, UpdateBookingDto updateDto);
        Task UpdateBookingStatusAsync(int id, string newStatusString);
        Task UpdateBookingStatusAsync(int id, UpdateBookingDto updateDto);
        Task UpdateBookingTherapistAsync(int bookingId, int newTherapistId);
        Task DeleteBookingAsync(int id);
    }
}
