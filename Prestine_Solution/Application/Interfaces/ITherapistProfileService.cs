using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITherapistProfileService
    {
        Task<IEnumerable<TherapistProfileDto>> GetAllProfilesAsync();
        Task<TherapistProfileDto> GetProfileByProfileIdAsync(int profileId); 
        Task<TherapistProfileDto> GetProfileByTherapistIdAsync(int therapistId);
        Task<IEnumerable<TherapistProfileDto>> GetAllProfilesWithReferenceAsync();
        Task<TherapistProfileDto> GetProfileWithReferenceByProfileIdAsync(int profileId);
        Task<TherapistProfileDto> GetProfileWithReferenceByTherapistIdAsync(int therapistId);
        Task<TherapistProfileDto> CreateProfileAsync(int therapistId, CreateTherapistProfileDto dto);
        Task<TherapistProfileDto> UpdateProfileAsync(int therapistId, UpdateTherapistProfileDto dto);
        Task DeleteProfileAsync(int therapistId);
        
    }
}
