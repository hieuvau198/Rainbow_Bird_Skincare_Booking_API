using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceCategoryService
    {
        Task<IEnumerable<ServiceCategoryDto>> GetAllCategoriesAsync();
        Task<ServiceCategoryDto> GetCategoryByIdAsync(int id);
        Task<ServiceCategoryDto> CreateCategoryAsync(CreateServiceCategoryDto createDto);
        Task UpdateCategoryAsync(int id, UpdateServiceCategoryDto updateDto);
        Task DeleteCategoryAsync(int id);
    }
}
