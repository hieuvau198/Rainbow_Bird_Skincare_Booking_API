using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceDto> GetServiceByIdAsync(int serviceId);
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync();
        Task<IEnumerable<ServiceDto>> GetServicesAsync(
            string serviceName = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string sortBy = "price",
            string order = "asc",
            int page = 1,
            int size = 10
        );
        Task<ServiceDto> CreateServiceAsync(CreateServiceDto dto);
        Task<ServiceDto> UpdateServiceAsync(int serviceId, UpdateServiceDto dto);
        Task DeleteServiceAsync(int serviceId);
    }
}
