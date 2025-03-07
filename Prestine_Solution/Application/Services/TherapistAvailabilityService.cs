﻿using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System.Linq.Expressions;

namespace Application.Services
{
    public class TherapistAvailabilityService : ITherapistAvailabilityService
    {
        private readonly IGenericRepository<TherapistAvailability> _availabilityRepository;
        private readonly IGenericRepository<Therapist> _therapistRepository;
        private readonly IGenericRepository<TimeSlot> _timeSlotRepository;
        private readonly IMapper _mapper;

        public TherapistAvailabilityService(
            IGenericRepository<TherapistAvailability> availabilityRepository,
            IGenericRepository<Therapist> therapistRepository,
            IGenericRepository<TimeSlot> timeSlotRepository,
            IMapper mapper)
        {
            _availabilityRepository = availabilityRepository;
            _therapistRepository = therapistRepository;
            _timeSlotRepository = timeSlotRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TherapistAvailabilityDto>> GetAllAvailabilitiesAsync()
        {
            var availabilities = await _availabilityRepository.GetAllAsync(null, t => t.Therapist, u => u.Therapist.User);
            return _mapper.Map<IEnumerable<TherapistAvailabilityDto>>(availabilities);
        }

        public async Task<TherapistAvailabilityDto> GetAvailabilityByIdAsync(int id)
        {
            var availability = await _availabilityRepository.GetByIdAsync(id);
            if (availability == null)
                throw new KeyNotFoundException($"Availability with ID {id} not found");

            return _mapper.Map<TherapistAvailabilityDto>(availability);
        }

        public async Task<IEnumerable<TherapistAvailabilityDto>> GetTherapistAvailabilitiesAsync(int therapistId, DateOnly? date = null)
        {
            var availabilities = await _availabilityRepository.GetAllAsync();
            var filtered = availabilities.Where(a => a.TherapistId == therapistId);

            if (date.HasValue)
                filtered = filtered.Where(a => a.WorkingDate == date.Value);

            return _mapper.Map<IEnumerable<TherapistAvailabilityDto>>(filtered);
        }

        public async Task<IEnumerable<TherapistAvailabilityDto>> GetAvailabilitiesBySlotIdAsync(int slotId, DateOnly? date = null)
        {
            Expression<Func<TherapistAvailability, bool>> predicate;

            if (date.HasValue)
                predicate = a => a.SlotId == slotId && a.WorkingDate == date.Value;
            else
                predicate = a => a.SlotId == slotId;

            // Include Therapist and User to get access to the name
            var availabilities = await _availabilityRepository.GetAllAsync(
                predicate,
                a => a.Therapist,
                a => a.Therapist.User);

            return _mapper.Map<IEnumerable<TherapistAvailabilityDto>>(availabilities);
        }

        public async Task<TherapistAvailabilityDto> CreateAvailabilityAsync(CreateTherapistAvailabilityDto createDto)
        {
            // Verify therapist exists
            var therapist = await _therapistRepository.GetByIdAsync(createDto.TherapistId);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {createDto.TherapistId} not found");

            // Verify time slot exists
            var timeSlot = await _timeSlotRepository.GetByIdAsync(createDto.SlotId);
            if (timeSlot == null)
                throw new KeyNotFoundException($"TimeSlot with ID {createDto.SlotId} not found");

            // Check if availability already exists
            var existingAvailabilities = await _availabilityRepository.GetAllAsync();
            var exists = existingAvailabilities.Any(a =>
                a.TherapistId == createDto.TherapistId &&
                a.SlotId == createDto.SlotId &&
                a.WorkingDate == createDto.WorkingDate);

            if (exists)
                throw new InvalidOperationException("Availability already exists for this time slot and date");

            var availability = _mapper.Map<TherapistAvailability>(createDto);
            availability.Status = "Available";
            availability.CreatedAt = DateTime.UtcNow;

            await _availabilityRepository.CreateAsync(availability);
            return _mapper.Map<TherapistAvailabilityDto>(availability);
        }

        public async Task UpdateAvailabilityAsync(int id, UpdateTherapistAvailabilityDto updateDto)
        {
            var availability = await _availabilityRepository.GetByIdAsync(id);
            if (availability == null)
                throw new KeyNotFoundException($"Availability with ID {id} not found");

            _mapper.Map(updateDto, availability);
            await _availabilityRepository.UpdateAsync(availability);
        }

        public async Task DeleteAvailabilityAsync(int id)
        {
            var availability = await _availabilityRepository.GetByIdAsync(id);
            if (availability == null)
                throw new KeyNotFoundException($"Availability with ID {id} not found");

            await _availabilityRepository.DeleteAsync(availability);
        }
    }
}