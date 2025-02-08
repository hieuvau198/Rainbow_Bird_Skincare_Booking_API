using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWorkingDayService
    {
        Task<IEnumerable<WorkingDayDto>> GetAllWorkingDaysAsync();
        Task<WorkingDayDto> GetWorkingDayByIdAsync(int id);
        Task<WorkingDayDto> GetWorkingDayByNameAsync(string dayName);
        Task<WorkingDayDto> CreateWorkingDayAsync(CreateWorkingDayDto createDto);
        Task UpdateWorkingDayAsync(int id, UpdateWorkingDayDto updateDto);
        Task DeleteWorkingDayAsync(int id);
        Task<bool> IsWorkingDayExistsAsync(string dayName);
    }
}
