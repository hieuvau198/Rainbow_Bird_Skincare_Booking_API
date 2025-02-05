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
    public class ServiceService : IServiceService
    {
        private readonly IGenericRepository<Service> _repository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ServiceService(
            IGenericRepository<Service> repository,
            IImageService imageService,
            IMapper mapper)
        {
            _repository = repository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<ServiceDto> GetServiceByIdAsync(int serviceId)
        {
            var services = await _repository.GetAllAsync();
            var service = services.FirstOrDefault(s => s.ServiceId == serviceId);

            if (service == null)
                throw new KeyNotFoundException($"Service not found with ID: {serviceId}");

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            var services = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto dto)
        {
            var service = _mapper.Map<Service>(dto);
            service.CreatedAt = DateTime.UtcNow;

            // Handle service image
            if (dto.ServiceImage != null)
            {
                service.ServiceImage = await _imageService.UploadImageAsync(dto.ServiceImage);
            }

            var createdService = await _repository.CreateAsync(service);
            return _mapper.Map<ServiceDto>(createdService);
        }

        public async Task<ServiceDto> UpdateServiceAsync(int serviceId, UpdateServiceDto dto)
        {
            var services = await _repository.GetAllAsync();
            var existingService = services.FirstOrDefault(s => s.ServiceId == serviceId);

            if (existingService == null)
                throw new KeyNotFoundException($"Service not found with ID: {serviceId}");

            // Handle service image update
            if (dto.ServiceImage != null)
            {
                // Delete old image if it exists
                if (!string.IsNullOrEmpty(existingService.ServiceImage))
                {
                    await _imageService.DeleteImageAsync(existingService.ServiceImage);
                }

                existingService.ServiceImage = await _imageService.UploadImageAsync(dto.ServiceImage);
            }

            // Update other properties
            _mapper.Map(dto, existingService);

            await _repository.UpdateAsync(existingService);
            return _mapper.Map<ServiceDto>(existingService);
        }

        public async Task DeleteServiceAsync(int serviceId)
        {
            var services = await _repository.GetAllAsync();
            var service = services.FirstOrDefault(s => s.ServiceId == serviceId);

            if (service == null)
                throw new KeyNotFoundException($"Service not found with ID: {serviceId}");

            // Delete service image if it exists
            if (!string.IsNullOrEmpty(service.ServiceImage))
            {
                await _imageService.DeleteImageAsync(service.ServiceImage);
            }

            await _repository.DeleteAsync(service);
        }
    }
}
