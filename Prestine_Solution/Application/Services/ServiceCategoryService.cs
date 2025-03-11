using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly IGenericRepository<ServiceCategory> _repository;
        private readonly IMapper _mapper;

        public ServiceCategoryService(IGenericRepository<ServiceCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceCategoryDto>>(categories);
        }

        public async Task<ServiceCategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("The requested category does not exist.");

            return _mapper.Map<ServiceCategoryDto>(category);
        }

        public async Task<ServiceCategoryDto> CreateCategoryAsync(CreateServiceCategoryDto createDto)
        {
            var category = _mapper.Map<ServiceCategory>(createDto);
            await _repository.CreateAsync(category);
            return _mapper.Map<ServiceCategoryDto>(category);
        }

        public async Task UpdateCategoryAsync(int id, UpdateServiceCategoryDto updateDto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("The requested category does not exist.");

            _mapper.Map(updateDto, category);
            await _repository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("The requested category does not exist.");

            await _repository.DeleteAsync(category);
        }
    }
}
