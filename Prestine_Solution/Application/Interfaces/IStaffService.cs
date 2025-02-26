using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> GetAllStaffAsync();
        Task<StaffDto> GetStaffByIdAsync(int id);
        Task<StaffDto> GetStaffByUserIdAsync(int userId);
        Task<StaffDto> CreateStaffAsync(CreateStaffDto createDto);
        Task<StaffDto> CreateStaffWithUserAsync(CreateStaffUserDto createDto);
        Task UpdateStaffAsync(int id, UpdateStaffDto updateDto);
        Task DeleteStaffAsync(int id);
    }
}
