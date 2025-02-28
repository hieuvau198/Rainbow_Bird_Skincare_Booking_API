using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITherapistService
    {
        Task<IEnumerable<TherapistDto>> GetTherapistsAsync();
        Task<IEnumerable<TherapistDto>> GetTherapistsWithReferenceAsync();
        Task<TherapistDto> GetByIdAsync(int id);
        Task<TherapistDto> GetByIdWithReferenceAsync(int id);
        Task<TherapistDto> CreateTherapistAsync(CreateTherapistDto createTherapistDto);
        Task<TherapistDto> CreateTherapistWithUserAsync(CreateTherapistUserDto createDto);
        Task UpdateTherapistAsync(int id, UpdateTherapistDto updateTherapistDto);
        Task DeleteTherapistAsync(int id);
    }
}
