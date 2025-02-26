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
        Task<IEnumerable<TherapistDto>> GetAllTherapistsAsync();
        Task<TherapistDto> GetTherapistByIdAsync(int id);
        Task<TherapistDto> CreateTherapistAsync(CreateTherapistDto createTherapistDto);
        Task<TherapistDto> CreateTherapistWithUserAsync(CreateTherapistUserDto createDto);
        Task UpdateTherapistAsync(int id, UpdateTherapistDto updateTherapistDto);
        Task DeleteTherapistAsync(int id);
    }
}
