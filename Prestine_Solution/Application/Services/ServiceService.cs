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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ServiceService(
            IUnitOfWork unitOfWork,
            IImageService imageService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<ServiceDto> GetServiceByIdAsync(int serviceId)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(serviceId, s => s.Category);
            if (service == null)
                throw new KeyNotFoundException($"Service not found with ID: {serviceId}");
            if (!service.IsActive ?? false)
                throw new KeyNotFoundException($"This service is not exist anymore: {serviceId}");

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            var services = await _unitOfWork.Services.GetAllAsync(s => s.IsActive == true, s => s.Category);

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
            var query = _unitOfWork.Services.GetAllAsQueryable();

            query = query.Where(s => s.IsActive == true);

            if (!string.IsNullOrEmpty(serviceName))
                query = query.Where(s => s.ServiceName.Contains(serviceName));

            if (minPrice.HasValue)
                query = query.Where(s => s.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(s => s.Price <= maxPrice.Value);

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
            if (dto.CategoryId == 0)
            {
                throw new KeyNotFoundException("Please specify Category");
            }
            var category = await _unitOfWork.ServiceCategories.GetByIdAsync(dto.CategoryId ?? 0);
            if (category == null)
                throw new KeyNotFoundException("Invalid category ID");

            var service = _mapper.Map<Service>(dto);
            service.CreatedAt = DateTime.UtcNow;
            service.Category = category;
            service.IsActive = true;

            if (dto.ServiceImage != null)
            {
                service.ServiceImage = await _imageService.UploadImageAsync(dto.ServiceImage);
            }
            else
            {
                service.ServiceImage = "https://www.theskinclinics.com/wp-content/uploads/2022/11/fader3.jpg";
            }

            await _unitOfWork.Services.CreateAsync(service);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> UpdateServiceAsync(int serviceId, UpdateServiceDto dto)
        {
            var existingService = await _unitOfWork.Services.GetByIdAsync(serviceId, s => s.Category);
            if (existingService == null)
                throw new KeyNotFoundException($"Service not found with ID: {serviceId}");

            if (!existingService.IsActive ?? false)
                throw new KeyNotFoundException("This service is not exist anymore");

            dto.IsActive = true;

            if (dto.ServiceImage != null)
            {
                if (!string.IsNullOrEmpty(existingService.ServiceImage))
                {
                    await _imageService.DeleteImageAsync(existingService.ServiceImage);
                }

                existingService.ServiceImage = await _imageService.UploadImageAsync(dto.ServiceImage);
            }

            if (dto.CategoryId.HasValue)
            {
                var newCategory = await _unitOfWork.ServiceCategories.GetByIdAsync(dto.CategoryId.Value);
                if (newCategory == null)
                    throw new KeyNotFoundException("Invalid category ID");

                existingService.Category = newCategory;
            }

            _mapper.Map(dto, existingService);

            await _unitOfWork.Services.UpdateAsync(existingService);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceDto>(existingService);
        }

        public async Task DeleteServiceAsync(int serviceId)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(serviceId);
            if (service == null)
                throw new KeyNotFoundException($"Service not found with ID: {serviceId}");

            service.IsActive = false;

            await _unitOfWork.Services.UpdateAsync(service);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}