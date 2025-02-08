using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<TimeSlotDto>> GetAllTimeSlotsAsync();
        Task<TimeSlotDto> GetTimeSlotByIdAsync(int id);
        Task<IEnumerable<TimeSlotDto>> GetTimeSlotsByWorkingDayAsync(int workingDayId);
        Task<TimeSlotDto> CreateTimeSlotAsync(CreateTimeSlotDto createDto);
        Task UpdateTimeSlotAsync(int id, UpdateTimeSlotDto updateDto);
        Task DeleteTimeSlotAsync(int id);
    }
}
