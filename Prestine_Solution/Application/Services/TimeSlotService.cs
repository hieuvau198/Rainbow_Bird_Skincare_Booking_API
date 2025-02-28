using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;

namespace Application.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly IGenericRepository<TimeSlot> _repository;
        private readonly IGenericRepository<WorkingDay> _workingDayRepository;
        private readonly IMapper _mapper;

        public TimeSlotService(
            IGenericRepository<TimeSlot> repository,
            IGenericRepository<WorkingDay> workingDayRepository,
            IMapper mapper)
        {
            _repository = repository;
            _workingDayRepository = workingDayRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimeSlotDto>> GetAllTimeSlotsAsync()
        {
            var timeSlots = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TimeSlotDto>>(timeSlots);
        }

        public async Task<TimeSlotDto> GetTimeSlotByIdAsync(int id)
        {
            var timeSlot = await _repository.GetByIdAsync(id);
            if (timeSlot == null)
                throw new KeyNotFoundException($"Time slot with ID {id} not found");

            return _mapper.Map<TimeSlotDto>(timeSlot);
        }

        public async Task<IEnumerable<TimeSlotDto>> GetTimeSlotsByWorkingDayAsync(int workingDayId)
        {
            var timeSlots = await _repository.GetAllAsync(
                t => t.WorkingDayId == workingDayId // ✅ Filtering happens in the database
            );

            return _mapper.Map<IEnumerable<TimeSlotDto>>(timeSlots.OrderBy(t => t.SlotNumber));
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

            var timeSlot = _mapper.Map<TimeSlot>(createDto);
            timeSlot.CreatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(timeSlot);
            return _mapper.Map<TimeSlotDto>(timeSlot);
        }

        public async Task UpdateTimeSlotAsync(int id, UpdateTimeSlotDto updateDto)
        {
            var timeSlot = await _repository.GetByIdAsync(id);
            if (timeSlot == null)
                throw new KeyNotFoundException($"Time slot with ID {id} not found");

            var workingDay = await _workingDayRepository.GetByIdAsync(timeSlot.WorkingDayId);
            if (workingDay == null)
                throw new KeyNotFoundException($"Working day with ID {timeSlot.WorkingDayId} not found");

            _mapper.Map(updateDto, timeSlot);

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
    }
}