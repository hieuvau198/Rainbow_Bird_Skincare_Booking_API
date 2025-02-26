using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            return Ok(customer);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByUserId(int userId)
        {
            var customer = await _customerService.GetCustomerByUserIdAsync(userId);
            return Ok(customer);
        }

        [HttpGet("by-visit-date")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersByLastVisitDate([FromQuery] DateTime date)
        {
            var customers = await _customerService.GetCustomersByLastVisitDateAsync(date);
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CreateCustomerDto createDto)
        {
            var customer = await _customerService.CreateCustomerAsync(createDto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updateDto)
        {
            await _customerService.UpdateCustomerAsync(id, updateDto);
            return NoContent();
        }

        [HttpPut("{id}/last-visit")]
        public async Task<IActionResult> UpdateLastVisitDate(int id, [FromBody] DateTime lastVisitDate)
        {
            await _customerService.UpdateLastVisitDateAsync(id, lastVisitDate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
