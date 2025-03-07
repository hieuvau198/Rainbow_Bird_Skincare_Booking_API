using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerFeedbackService
    {
        Task<IEnumerable<CustomerFeedbackDto>> GetAllFeedbacksAsync();
        Task<CustomerFeedbackDto> GetFeedbackByIdAsync(int id);
        Task<IEnumerable<CustomerFeedbackDto>> GetFeedbacksByBookingIdAsync(int bookingId);
        Task<CustomerFeedbackDto> CreateFeedbackAsync(CreateCustomerFeedbackDto createDto);
        Task UpdateFeedbackAsync(int id, UpdateCustomerFeedbackDto updateDto);
        Task DeleteFeedbackAsync(int id);
        Task<bool> FeedbackExistsAsync(int id);
        Task<bool> HasBookingFeedbackAsync(int bookingId);
        Task<IEnumerable<CustomerFeedbackDto>> GetRecentFeedbacksAsync(int count);
    }
}