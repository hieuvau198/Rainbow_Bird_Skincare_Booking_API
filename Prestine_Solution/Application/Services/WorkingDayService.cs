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
    public class WorkingDayService : IWorkingDayService
    {
        private readonly IGenericRepository<WorkingDay> _repository;
        private readonly IGenericRepository<TimeSlot> _timeSlotRepository;
        private readonly IMapper _mapper;

        public WorkingDayService(
            IGenericRepository<WorkingDay> repository,
            IGenericRepository<TimeSlot> timeSlotRepository,
            IMapper mapper)
        {
            _repository = repository;
            _timeSlotRepository = timeSlotRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkingDayDto>> GetAllWorkingDaysAsync()
        {
            // ✅ Fetch all working days with time slots in a single query
            var workingDays = await _repository.GetAllAsync(null, w => w.TimeSlots);
            return _mapper.Map<IEnumerable<WorkingDayDto>>(workingDays);
        }

        public async Task<WorkingDayDto> GetWorkingDayByIdAsync(int id)
        {
            // ✅ Fetch the working day with time slots in one query
            var workingDay = await _repository.GetByIdAsync(id, w => w.TimeSlots);

            if (workingDay == null)
                throw new KeyNotFoundException($"Working day with ID {id} not found");

            return _mapper.Map<WorkingDayDto>(workingDay);
        }

        public async Task<WorkingDayDto> GetWorkingDayByNameAsync(string dayName)
        {
            // ✅ Fetch the working day by name with time slots in one query
            var workingDay = await _repository.FindAsync(
                w => w.DayName != null && w.DayName.Equals(dayName, StringComparison.OrdinalIgnoreCase),
                w => w.TimeSlots
            );


            if (workingDay == null)
                throw new KeyNotFoundException($"Working day {dayName} not found");

            return _mapper.Map<WorkingDayDto>(workingDay);
        }

        public async Task<WorkingDayDto> CreateWorkingDayAsync(CreateWorkingDayDto createDto)
        {
            if (createDto.EndTime <= createDto.StartTime)
                throw new InvalidOperationException("End time must be after start time");

            if (await IsWorkingDayExistsAsync(createDto.DayName))
                throw new InvalidOperationException($"Working day {createDto.DayName} already exists");

            var workingDay = _mapper.Map<WorkingDay>(createDto);
            workingDay.CreatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(workingDay);
            return _mapper.Map<WorkingDayDto>(workingDay);
        }

        public async Task UpdateWorkingDayAsync(int id, UpdateWorkingDayDto updateDto)
        {
            var workingDay = await _repository.GetByIdAsync(id);
            if (workingDay == null)
                throw new KeyNotFoundException($"Working day with ID {id} not found");

            _mapper.Map(updateDto, workingDay);

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
            // ✅ Check if a working day exists without loading all data
            return await _repository.ExistsAsync(w => w.DayName != null && w.DayName.Equals(dayName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
