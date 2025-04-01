using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerRatingService
    {
        Task<IEnumerable<CustomerRatingDto>> GetAllRatingsAsync();
        Task<CustomerRatingDto> GetRatingByIdAsync(int id);
        Task<IEnumerable<CustomerRatingDto>> GetRatingsByBookingIdAsync(int bookingId);
        Task<IEnumerable<CustomerRatingDto>> GetRatingsByServiceIdAsync(int serviceId);
        Task<CustomerRatingDto> CreateRatingAsync(CreateCustomerRatingDto createDto);
        Task UpdateRatingAsync(int id, UpdateCustomerRatingDto updateDto);
        Task DeleteRatingAsync(int id);
        Task<bool> RatingExistsAsync(int id);
        Task<bool> HasUserRatedBookingAsync(int bookingId);
    }
}
