using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IManagerService
    {
        Task<IEnumerable<ManagerDto>> GetAllManagersAsync();
        Task<ManagerDto> GetManagerByIdAsync(int id);
        Task<ManagerDto> GetManagerByUserIdAsync(int userId);
        Task<ManagerDto> CreateManagerAsync(CreateManagerDto createDto);
        Task<ManagerDto> CreateManagerWithUserAsync(CreateManagerUserDto createDto);
        Task UpdateManagerAsync(int id, UpdateManagerDto updateDto);
        Task DeleteManagerAsync(int id);
    }
}
