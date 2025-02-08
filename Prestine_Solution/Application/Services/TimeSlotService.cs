using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly IGenericRepository<TimeSlot> _repository;
        private readonly IGenericRepository<WorkingDay> _workingDayRepository;

        public TimeSlotService(
            IGenericRepository<TimeSlot> repository,
            IGenericRepository<WorkingDay> workingDayRepository)
        {
            _repository = repository;
            _workingDayRepository = workingDayRepository;
        }

        public async Task<IEnumerable<TimeSlotDto>> GetAllTimeSlotsAsync()
        {
            var timeSlots = await _repository.GetAllAsync();
            return timeSlots.Select(MapToDto);
        }

        public async Task<TimeSlotDto> GetTimeSlotByIdAsync(int id)
        {
            var timeSlot = await _repository.GetByIdAsync(id);
            if (timeSlot == null)
                throw new KeyNotFoundException($"Time slot with ID {id} not found");

            return MapToDto(timeSlot);
        }

        public async Task<IEnumerable<TimeSlotDto>> GetTimeSlotsByWorkingDayAsync(int workingDayId)
        {
            var timeSlots = await _repository.GetAllAsync();
            return timeSlots.Where(t => t.WorkingDayId == workingDayId)
                          .OrderBy(t => t.SlotNumber)
                          .Select(MapToDto);
        }

        public async Task<TimeSlotDto> CreateTimeSlotAsync(CreateTimeSlotDto createDto)
        {
            // Verify working day exists
            var workingDay = await _workingDayRepository.GetByIdAsync(createDto.WorkingDayId);
            if (workingDay == null)
                throw new KeyNotFoundException($"Working day with ID {createDto.WorkingDayId} not found");

            // Validate time range
            if (createDto.EndTime <= createDto.StartTime)
                throw new InvalidOperationException("End time must be after start time");

            // Validate time slot is within working day hours
            if (createDto.StartTime < workingDay.StartTime || createDto.EndTime > workingDay.EndTime)
                throw new InvalidOperationException("Time slot must be within working day hours");

            var timeSlot = new TimeSlot
            {
                WorkingDayId = createDto.WorkingDayId,
                StartTime = createDto.StartTime,
                EndTime = createDto.EndTime,
                SlotNumber = createDto.SlotNumber,
                IsActive = createDto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(timeSlot);
            return MapToDto(timeSlot);
        }

        public async Task UpdateTimeSlotAsync(int id, UpdateTimeSlotDto updateDto)
        {
            var timeSlot = await _repository.GetByIdAsync(id);
            if (timeSlot == null)
                throw new KeyNotFoundException($"Time slot with ID {id} not found");

            var workingDay = await _workingDayRepository.GetByIdAsync(timeSlot.WorkingDayId);
            if (workingDay == null)
                throw new KeyNotFoundException($"Working day with ID {timeSlot.WorkingDayId} not found");

            if (updateDto.StartTime.HasValue)
                timeSlot.StartTime = updateDto.StartTime.Value;

            if (updateDto.EndTime.HasValue)
                timeSlot.EndTime = updateDto.EndTime.Value;

            if (updateDto.IsActive.HasValue)
                timeSlot.IsActive = updateDto.IsActive.Value;

            // Validate time range
            if (timeSlot.EndTime <= timeSlot.StartTime)
                throw new InvalidOperationException("End time must be after start time");

            // Validate time slot is within working day hours
            if (timeSlot.StartTime < workingDay.StartTime || timeSlot.EndTime > workingDay.EndTime)
                throw new InvalidOperationException("Time slot must be within working day hours");

            await _repository.UpdateAsync(timeSlot);
        }

        public async Task DeleteTimeSlotAsync(int id)
        {
            var timeSlot = await _repository.GetByIdAsync(id);
            if (timeSlot == null)
                throw new KeyNotFoundException($"Time slot with ID {id} not found");

            await _repository.DeleteAsync(timeSlot);
        }

        public TimeSlotDto MapToDto(TimeSlot timeSlot)
        {
            return new TimeSlotDto
            {
                SlotId = timeSlot.SlotId,
                WorkingDayId = timeSlot.WorkingDayId,
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime,
                SlotNumber = timeSlot.SlotNumber,
                IsActive = timeSlot.IsActive,
                CreatedAt = timeSlot.CreatedAt,
                WorkingDay = timeSlot.WorkingDay != null ? new WorkingDayDto
                {
                    WorkingDayId = timeSlot.WorkingDay.WorkingDayId,
                    DayName = timeSlot.WorkingDay.DayName,
                    StartTime = timeSlot.WorkingDay.StartTime,
                    EndTime = timeSlot.WorkingDay.EndTime,
                    SlotDurationMinutes = timeSlot.WorkingDay.SlotDurationMinutes,
                    IsActive = timeSlot.WorkingDay.IsActive,
                    CreatedAt = timeSlot.WorkingDay.CreatedAt
                } : null
            };
        }
    }
}