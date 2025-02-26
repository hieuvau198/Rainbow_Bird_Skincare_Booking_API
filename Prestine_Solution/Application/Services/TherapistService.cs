using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TherapistService : ITherapistService
    {
        private readonly IGenericRepository<Therapist> _repository;
        private readonly IMapper _mapper;

        public TherapistService(IGenericRepository<Therapist> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TherapistDto>> GetAllTherapistsAsync()
        {
            var therapists = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TherapistDto>>(therapists);
        }

        public async Task<TherapistDto> GetTherapistByIdAsync(int id)
        {
            var therapist = await _repository.GetByIdAsync(id);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            return _mapper.Map<TherapistDto>(therapist);
        }

        public async Task<TherapistDto> CreateTherapistAsync(CreateTherapistDto createDto)
        {
            var therapist = _mapper.Map<Therapist>(createDto);
            await _repository.CreateAsync(therapist);
            return _mapper.Map<TherapistDto>(therapist);
        }

        public async Task UpdateTherapistAsync(int id, UpdateTherapistDto updateDto)
        {
            var therapist = await _repository.GetByIdAsync(id);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            _mapper.Map(updateDto, therapist);
            await _repository.UpdateAsync(therapist);
        }

        public async Task DeleteTherapistAsync(int id)
        {
            var therapist = await _repository.GetByIdAsync(id);
            if (therapist == null)
                throw new KeyNotFoundException($"Therapist with ID {id} not found");

            await _repository.DeleteAsync(therapist);
        }
    }
}