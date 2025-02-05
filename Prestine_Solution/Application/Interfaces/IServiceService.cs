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
        Task<ServiceDto> CreateServiceAsync(CreateServiceDto dto);
        Task<ServiceDto> UpdateServiceAsync(int serviceId, UpdateServiceDto dto);
        Task DeleteServiceAsync(int serviceId);
    }
}
