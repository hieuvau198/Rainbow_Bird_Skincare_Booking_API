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
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(
            IServiceService serviceService,
            ILogger<ServiceController> logger)
        {
            _serviceService = serviceService;
            _logger = logger;
        }

        [HttpGet]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServices()
        {
            try
            {
                var services = await _serviceService.GetAllServicesAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all services");
                return StatusCode(500, "An error occurred while retrieving services");
            }
        }

        [HttpGet("{serviceId}")]
        //[Authorize(Policy = "OpenPolicy")]
        public async Task<ActionResult<ServiceDto>> GetService(int serviceId)
        {
            try
            {
                var service = await _serviceService.GetServiceByIdAsync(serviceId);
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service for ID: {ServiceId}", serviceId);
                return StatusCode(500, "An error occurred while retrieving the service");
            }
        }

        [HttpPost]
        //[Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<ServiceDto>> CreateService([FromForm] CreateServiceDto createDto)
        {
            try
            {
                var service = await _serviceService.CreateServiceAsync(createDto);
                return CreatedAtAction(
                    nameof(GetService),
                    new { serviceId = service.ServiceId },
                    service);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating service");
                return StatusCode(500, "An error occurred while creating the service");
            }
        }

        [HttpPut("{serviceId}")]
        //[Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<ServiceDto>> UpdateService(
            int serviceId,
            [FromForm] UpdateServiceDto updateDto)
        {
            try
            {
                var service = await _serviceService.UpdateServiceAsync(serviceId, updateDto);
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating service for ID: {ServiceId}", serviceId);
                return StatusCode(500, "An error occurred while updating the service");
            }
        }

        [HttpDelete("{serviceId}")]
        //[Authorize(Policy = "RestrictPolicy")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            try
            {
                await _serviceService.DeleteServiceAsync(serviceId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting service for ID: {ServiceId}", serviceId);
                return StatusCode(500, "An error occurred while deleting the service");
            }
        }
    }
}
