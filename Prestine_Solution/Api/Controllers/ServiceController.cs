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

        [HttpGet("{serviceId}")]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<ServiceDto>> GetService(int serviceId)
        {
            var service = await _serviceService.GetServiceByIdAsync(serviceId);
            return service != null ? Ok(service) : NotFound();
        }

        [HttpPost]
        //[Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<ServiceDto>> CreateService([FromForm] CreateServiceDto createDto)
        {
            var service = await _serviceService.CreateServiceAsync(createDto);
            return CreatedAtAction(nameof(GetService), new { serviceId = service.ServiceId }, service);
        }

        [HttpPut("{serviceId}")]
        //[Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<ServiceDto>> UpdateService(int serviceId, [FromForm] UpdateServiceDto updateDto)
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
