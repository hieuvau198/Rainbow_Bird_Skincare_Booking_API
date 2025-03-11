using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryService _service;

        public ServiceCategoryController(IServiceCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceCategoryDto>>> GetAllCategories()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceCategoryDto>> GetCategoryById(int id)
        {
            try
            {
                var category = await _service.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceCategoryDto>> CreateCategory(CreateServiceCategoryDto createDto)
        {
            var category = await _service.CreateCategoryAsync(createDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateServiceCategoryDto updateDto)
        {
            try
            {
                await _service.UpdateCategoryAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _service.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
