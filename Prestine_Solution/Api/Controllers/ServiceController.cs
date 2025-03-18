using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServices()
        {
            try
            {
                var services = await _serviceService.GetAllServicesAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving services.", error = ex.Message });
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetServices(
            [FromQuery] string serviceName = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] string sortBy = "price",
            [FromQuery] string order = "asc",
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            try
            {
                var services = await _serviceService.GetServicesAsync(serviceName, minPrice, maxPrice, categoryId, sortBy, order, page, size);
                return Ok(new
                {
                    totalItems = services.Count(),
                    currentPage = page,
                    pageSize = size,
                    data = services
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving services.", error = ex.Message });
            }
        }

        [HttpGet("{serviceId}")]
        public async Task<ActionResult<ServiceDto>> GetService(int serviceId)
        {
            try
            {
                var service = await _serviceService.GetServiceByIdAsync(serviceId);
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error retrieving service.", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<ServiceDto>> CreateService([FromForm] CreateServiceDto createDto)
        {
            try
            {
                var service = await _serviceService.CreateServiceAsync(createDto);
                return CreatedAtAction(nameof(GetService), new { serviceId = service.ServiceId }, service);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error creating service.", error = ex.Message });
            }
        }

        [HttpPut("{serviceId}")]
        [Authorize(Policy = "StandardPolicy")]
        public async Task<ActionResult<ServiceDto>> UpdateService(int serviceId, [FromForm] UpdateServiceDto updateDto)
        {
            try
            {
                var service = await _serviceService.UpdateServiceAsync(serviceId, updateDto);
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error updating service.", error = ex.Message });
            }
        }

        [HttpDelete("{serviceId}")]
        [Authorize(Policy = "RestrictPolicy")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            try
            {
                await _serviceService.DeleteServiceAsync(serviceId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error deleting service.", error = ex.Message });
            }
        }
    }
}
