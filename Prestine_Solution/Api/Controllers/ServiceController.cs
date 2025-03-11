using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServices()
        {
            return Ok(await _serviceService.GetAllServicesAsync());
        }

        [HttpGet("filter")]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<IActionResult> GetServices(
            [FromQuery] string serviceName = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string sortBy = "price",
            [FromQuery] string order = "asc",
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var services = await _serviceService.GetServicesAsync(serviceName, minPrice, maxPrice, sortBy, order, page, size);

            return Ok(new
            {
                totalItems = services.Count(),
                currentPage = page,
                pageSize = size,
                data = services
            });
        }

        [HttpGet("{serviceId}")]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<ServiceDto>> GetService(int serviceId)
        {
            var service = await _serviceService.GetServiceByIdAsync(serviceId);
            return service != null ? Ok(service) : NotFound();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetServices(
        //    [FromQuery] string serviceName = null,
        //    [FromQuery] decimal? minPrice = null,
        //    [FromQuery] decimal? maxPrice = null,
        //    [FromQuery] string sortBy = "price",
        //    [FromQuery] string order = "asc",
        //    [FromQuery] int page = 1,
        //    [FromQuery] int size = 10)
        //{
        //    var services = await _serviceService.GetServicesAsync(serviceName, minPrice, maxPrice, sortBy, order, page, size);

        //    return Ok(new
        //    {
        //        totalItems = services.Count(),
        //        currentPage = page,
        //        pageSize = size,
        //        data = services
        //    });
        //}

        [HttpPost]
        //[Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<ServiceDto>> CreateService([FromForm] CreateServiceDto createDto)
        {
            var service = await _serviceService.CreateServiceAsync(createDto);
            return CreatedAtAction(nameof(GetService), new { serviceId = service.ServiceId }, service);
        }

        [HttpPut("{serviceId}")]
        //[Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<ServiceDto>> UpdateService(int serviceId, [FromBody] UpdateServiceDto updateDto)
        {
            var service = await _serviceService.UpdateServiceAsync(serviceId, updateDto);
            return service != null ? Ok(service) : NotFound();
        }

        [HttpDelete("{serviceId}")]
        //[Authorize(Policy = "RestrictPolicy")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            await _serviceService.DeleteServiceAsync(serviceId);
            return NoContent();
        }
    }
}
