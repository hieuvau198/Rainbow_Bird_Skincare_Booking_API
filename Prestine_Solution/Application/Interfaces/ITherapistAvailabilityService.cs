using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITherapistAvailabilityService
    {
        Task<IEnumerable<TherapistAvailabilityDto>> GetAllAvailabilitiesAsync();
        Task<TherapistAvailabilityDto> GetAvailabilityByIdAsync(int id);
        Task<IEnumerable<TherapistAvailabilityDto>> GetTherapistAvailabilitiesAsync(int therapistId, DateOnly? date = null);
        // New method to get availabilities by slot ID
        Task<IEnumerable<TherapistAvailabilityDto>> GetAvailabilitiesBySlotIdAsync(int slotId, DateOnly? date = null);
        Task<TherapistAvailabilityDto> CreateAvailabilityAsync(CreateTherapistAvailabilityDto createDto);
        Task UpdateAvailabilityAsync(int id, UpdateTherapistAvailabilityDto updateDto);
        Task DeleteAvailabilityAsync(int id);
    }
}