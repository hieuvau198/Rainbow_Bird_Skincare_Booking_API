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
    public class TherapistAvailabilityService : ITherapistAvailabilityService
    {
        private readonly IGenericRepository<TherapistAvailability> _repository;
        private readonly ITherapistService _therapistService;
        private readonly ITimeSlotService _timeSlotService;
        private readonly IWorkingDayService _workingDayService;

        public TherapistAvailabilityService(
            IGenericRepository<TherapistAvailability> repository,
            ITherapistService therapistService,
            ITimeSlotService timeSlotService,
            IWorkingDayService workingDayService)
        {
            _repository = repository;
            _therapistService = therapistService;
            _timeSlotService = timeSlotService;
            _workingDayService = workingDayService;
        }

        public async Task<IEnumerable<TherapistAvailabilityDto>> GetAllAvailabilitiesAsync()
        {
            var availabilities = await _repository.GetAllAsync();
            return availabilities.Select(MapToDto);
        }

        public async Task<TherapistAvailabilityDto> GetAvailabilityByIdAsync(int id)
        {
            var availability = await _repository.GetByIdAsync(id);
            if (availability == null)
                throw new KeyNotFoundException($"Availability with ID {id} not found");

            return MapToDto(availability);
        }

        public async Task<IEnumerable<TherapistAvailabilityDto>> GetTherapistAvailabilitiesAsync(int therapistId, DateOnly? date = null)
        {
            var availabilities = await _repository.GetAllAsync();
            var filtered = availabilities.Where(a => a.TherapistId == therapistId);

            if (date.HasValue)
                filtered = filtered.Where(a => a.WorkingDate == date.Value);

            return filtered.Select(MapToDto);
        }

        public async Task<TherapistAvailabilityDto> CreateAvailabilityAsync(CreateTherapistAvailabilityDto createDto)
        {
            // Verify therapist exists
            await _therapistService.GetTherapistByIdAsync(createDto.TherapistId);

            // Verify time slot exists and is active
            var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(createDto.SlotId);
            if (timeSlot.IsActive != true)
                throw new InvalidOperationException("Cannot create availability for inactive time slot");

            // Get working day for the slot to verify day matches
            var workingDay = await _workingDayService.GetWorkingDayByIdAsync(timeSlot.WorkingDayId);
            if (!workingDay.IsActive == true)
                throw new InvalidOperationException("Cannot create availability for inactive working day");

            // Verify the working day matches the date
            if (workingDay.DayName.ToLower() != createDto.WorkingDate.DayOfWeek.ToString().ToLower())
                throw new InvalidOperationException("Working day does not match the provided date");

            // Check if availability already exists
            var existingAvailabilities = await _repository.GetAllAsync();
            var exists = existingAvailabilities.Any(a =>
                a.TherapistId == createDto.TherapistId &&
                a.SlotId == createDto.SlotId &&
                a.WorkingDate == createDto.WorkingDate);

            if (exists)
                throw new InvalidOperationException("Availability already exists for this time slot and date");

            var availability = new TherapistAvailability
            {
                TherapistId = createDto.TherapistId,
                SlotId = createDto.SlotId,
                WorkingDate = createDto.WorkingDate,
                Status = "Available",
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(availability);
            return MapToDto(availability);
        }

        public async Task UpdateAvailabilityAsync(int id, UpdateTherapistAvailabilityDto updateDto)
        {
            var availability = await _repository.GetByIdAsync(id);
            if (availability == null)
                throw new KeyNotFoundException($"Availability with ID {id} not found");

            if (updateDto.Status != null)
                availability.Status = updateDto.Status;

            await _repository.UpdateAsync(availability);
        }

        public async Task DeleteAvailabilityAsync(int id)
        {
            var availability = await _repository.GetByIdAsync(id);
            if (availability == null)
                throw new KeyNotFoundException($"Availability with ID {id} not found");

            await _repository.DeleteAsync(availability);
        }

        private TherapistAvailabilityDto MapToDto(TherapistAvailability availability)
        {
            return new TherapistAvailabilityDto
            {
                AvailabilityId = availability.AvailabilityId,
                TherapistId = availability.TherapistId,
                SlotId = availability.SlotId,
                WorkingDate = availability.WorkingDate,
                Status = availability.Status,
                CreatedAt = availability.CreatedAt,
                Slot = availability.Slot != null ? _timeSlotService.MapToDto(availability.Slot) : null
            };
        }
    }
}