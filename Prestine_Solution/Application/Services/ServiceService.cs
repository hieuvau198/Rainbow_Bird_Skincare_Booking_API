using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IGenericRepository<Service> _repository;
        private readonly IGenericRepository<ServiceCategory> _categoryRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ServiceService(
            IGenericRepository<Service> repository,
            IGenericRepository<ServiceCategory> categoryRepository,
            IImageService imageService,
            IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<ServiceDto> GetServiceByIdAsync(int serviceId)
        {
            var service = await _repository.GetByIdAsync(serviceId, s => s.Category);
            if (service == null)
                throw new KeyNotFoundException($"Service not found with ID: {serviceId}");

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            var services = await _repository.GetAllAsync(null, s => s.Category);
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<IEnumerable<ServiceDto>> GetServicesAsync(
            string serviceName = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? categoryId = null,
            string sortBy = "price",
            string order = "asc",
            int page = 1,
            int size = 10)
        {
            var query = _repository.GetAllAsQueryable();

            if (!string.IsNullOrEmpty(serviceName))
                query = query.Where(s => s.ServiceName.Contains(serviceName));

            if (minPrice.HasValue)
                query = query.Where(s => s.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(s => s.Price <= maxPrice.Value);

            // ✅ Filter by Category
            if (categoryId.HasValue)
                query = query.Where(s => s.CategoryId == categoryId.Value);

            query = sortBy.ToLower() switch
            {
                "price" => order.ToLower() == "desc" ? query.OrderByDescending(s => s.Price) : query.OrderBy(s => s.Price),
                "name" => order.ToLower() == "desc" ? query.OrderByDescending(s => s.ServiceName) : query.OrderBy(s => s.ServiceName),
                "createdAt" => order.ToLower() == "desc" ? query.OrderByDescending(s => s.CreatedAt) : query.OrderBy(s => s.CreatedAt),
                _ => query.OrderBy(s => s.ServiceId)
            };

            var services = await query.Skip((page - 1) * size).Take(size).Include(s => s.Category).ToListAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto dto)
        {
            // ✅ Ensure the category exists
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId ?? 999);
            if (category == null)
                throw new KeyNotFoundException("Invalid category ID");

            var service = _mapper.Map<Service>(dto);
            service.CreatedAt = DateTime.UtcNow;
            service.Category = category;

            if (dto.ServiceImage != null)
            {
                service.ServiceImage = await _imageService.UploadImageAsync(dto.ServiceImage);
            }
            else
            {
                service.ServiceImage = "https://www.theskinclinics.com/wp-content/uploads/2022/11/fader3.jpg";
            }

            await _repository.CreateAsync(service);
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> UpdateServiceAsync(int serviceId, UpdateServiceDto dto)
        {
            var existingService = await _repository.GetByIdAsync(serviceId, s => s.Category);
            if (existingService == null)
                throw new KeyNotFoundException($"Service not found with ID: {serviceId}");

            if (dto.ServiceImage != null)
            {
                if (!string.IsNullOrEmpty(existingService.ServiceImage))
                {
                    await _imageService.DeleteImageAsync(existingService.ServiceImage);
                }

                existingService.ServiceImage = await _imageService.UploadImageAsync(dto.ServiceImage);
            }

            // ✅ Allow category updates
            if (dto.CategoryId.HasValue)
            {
                var newCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
                if (newCategory == null)
                    throw new KeyNotFoundException("Invalid category ID");

                existingService.Category = newCategory;
            }

            _mapper.Map(dto, existingService);
            await _repository.UpdateAsync(existingService);

            return _mapper.Map<ServiceDto>(existingService);
        }

        public async Task DeleteServiceAsync(int serviceId)
        {
            var service = await _repository.GetByIdAsync(serviceId);
            if (service == null)
                throw new KeyNotFoundException($"Service not found with ID: {serviceId}");

            if (!string.IsNullOrEmpty(service.ServiceImage))
            {
                await _imageService.DeleteImageAsync(service.ServiceImage);
            }

            await _repository.DeleteAsync(service);
        }
    }
}
