using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITherapistAvailabilityService
    {
        Task<IEnumerable<TherapistAvailabilityDto>> GetAllAvailabilitiesAsync();
        Task<TherapistAvailabilityDto> GetAvailabilityByIdAsync(int id);
        Task<IEnumerable<TherapistAvailabilityDto>> GetTherapistAvailabilitiesAsync(int therapistId, DateOnly? date = null);
        Task<TherapistAvailabilityDto> CreateAvailabilityAsync(CreateTherapistAvailabilityDto createDto);
        Task UpdateAvailabilityAsync(int id, UpdateTherapistAvailabilityDto updateDto);
        Task DeleteAvailabilityAsync(int id);
    }
}
