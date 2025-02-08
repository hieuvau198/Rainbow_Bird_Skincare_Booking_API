using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WorkingDayService : IWorkingDayService
    {
        private readonly IGenericRepository<WorkingDay> _repository;
        private readonly ITimeSlotService _timeSlotService;

        public WorkingDayService(
            IGenericRepository<WorkingDay> repository,
            ITimeSlotService timeSlotService)
        {
            _repository = repository;
            _timeSlotService = timeSlotService;
        }

        public async Task<IEnumerable<WorkingDayDto>> GetAllWorkingDaysAsync()
        {
            var workingDays = await _repository.GetAllAsync();
            return workingDays.Select(MapToDto);
        }

        public async Task<WorkingDayDto> GetWorkingDayByIdAsync(int id)
        {
            var workingDay = await _repository.GetByIdAsync(id);
            if (workingDay == null)
                throw new KeyNotFoundException($"Working day with ID {id} not found");

            return MapToDto(workingDay);
        }

        public async Task<WorkingDayDto> GetWorkingDayByNameAsync(string dayName)
        {
            var workingDays = await _repository.GetAllAsync();
            var workingDay = workingDays.FirstOrDefault(w => w.DayName.ToLower() == dayName.ToLower());

            if (workingDay == null)
                throw new KeyNotFoundException($"Working day {dayName} not found");

            return MapToDto(workingDay);
        }

        public async Task<WorkingDayDto> CreateWorkingDayAsync(CreateWorkingDayDto createDto)
        {
            // Validate time range
            if (createDto.EndTime <= createDto.StartTime)
                throw new InvalidOperationException("End time must be after start time");

            // Check if day already exists
            if (await IsWorkingDayExistsAsync(createDto.DayName))
                throw new InvalidOperationException($"Working day {createDto.DayName} already exists");

            var workingDay = new WorkingDay
            {
                DayName = createDto.DayName,
                StartTime = createDto.StartTime,
                EndTime = createDto.EndTime,
                SlotDurationMinutes = createDto.SlotDurationMinutes,
                IsActive = createDto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(workingDay);
            return MapToDto(workingDay);
        }

        public async Task UpdateWorkingDayAsync(int id, UpdateWorkingDayDto updateDto)
        {
            var workingDay = await _repository.GetByIdAsync(id);
            if (workingDay == null)
                throw new KeyNotFoundException($"Working day with ID {id} not found");

            if (updateDto.StartTime.HasValue)
                workingDay.StartTime = updateDto.StartTime.Value;

            if (updateDto.EndTime.HasValue)
                workingDay.EndTime = updateDto.EndTime.Value;

            if (updateDto.SlotDurationMinutes.HasValue)
                workingDay.SlotDurationMinutes = updateDto.SlotDurationMinutes.Value;

            if (updateDto.IsActive.HasValue)
                workingDay.IsActive = updateDto.IsActive.Value;

            // Validate time range
            if (workingDay.EndTime <= workingDay.StartTime)
                throw new InvalidOperationException("End time must be after start time");

            await _repository.UpdateAsync(workingDay);
        }

        public async Task DeleteWorkingDayAsync(int id)
        {
            var workingDay = await _repository.GetByIdAsync(id);
            if (workingDay == null)
                throw new KeyNotFoundException($"Working day with ID {id} not found");

            await _repository.DeleteAsync(workingDay);
        }

        public async Task<bool> IsWorkingDayExistsAsync(string dayName)
        {
            var workingDays = await _repository.GetAllAsync();
            return workingDays.Any(w => w.DayName.ToLower() == dayName.ToLower());
        }

        private WorkingDayDto MapToDto(WorkingDay workingDay)
        {
            return new WorkingDayDto
            {
                WorkingDayId = workingDay.WorkingDayId,
                DayName = workingDay.DayName,
                StartTime = workingDay.StartTime,
                EndTime = workingDay.EndTime,
                SlotDurationMinutes = workingDay.SlotDurationMinutes,
                IsActive = workingDay.IsActive,
                CreatedAt = workingDay.CreatedAt,
                TimeSlots = workingDay.TimeSlots.Select(_timeSlotService.MapToDto).ToList()
            };
        }
    }
}