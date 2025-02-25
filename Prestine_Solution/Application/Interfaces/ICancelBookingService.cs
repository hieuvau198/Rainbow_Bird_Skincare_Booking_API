using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICancelBookingService
    {
        Task<IEnumerable<CancelBookingDto>> GetAllCancelBookingsAsync();
        Task<CancelBookingDto> GetCancelBookingByIdAsync(int id);
        Task<CancelBookingDto> GetCancelBookingByBookingIdAsync(int bookingId);
        Task<CancelBookingDto> CreateCancelBookingAsync(CreateCancelBookingDto createDto);
        Task UpdateCancelBookingAsync(int id, UpdateCancelBookingDto updateDto);
        Task DeleteCancelBookingAsync(int id);
    }
}
