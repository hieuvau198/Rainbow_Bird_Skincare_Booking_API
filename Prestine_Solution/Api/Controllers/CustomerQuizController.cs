using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerQuizController : ControllerBase
    {
        private readonly ICustomerQuizService _customerQuizService;

        public CustomerQuizController(ICustomerQuizService customerQuizService)
        {
            _customerQuizService = customerQuizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerQuizDto>>> GetAllCustomerQuizzes()
        {
            return Ok(await _customerQuizService.GetAllCustomerQuizzesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerQuizDto>> GetCustomerQuizById(int id)
        {
            return Ok(await _customerQuizService.GetCustomerQuizByIdAsync(id));
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<CustomerQuizDto>>> GetCustomerQuizzesByCustomerId(int customerId)
        {
            return Ok(await _customerQuizService.GetCustomerQuizzesByCustomerIdAsync(customerId));
        }

        [HttpGet("customer/{customerId}/history")]
        public async Task<ActionResult<IEnumerable<CustomerQuizHistoryDto>>> GetCustomerQuizHistory(int customerId)
        {
            return Ok(await _customerQuizService.GetCustomerQuizHistoryAsync(customerId));
        }

        [HttpGet("customer/{customerId}/completed")]
        public async Task<ActionResult<IEnumerable<CustomerQuizDto>>> GetCompletedQuizzesByCustomerId(int customerId)
        {
            return Ok(await _customerQuizService.GetCompletedQuizzesByCustomerIdAsync(customerId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerQuizDto>> StartQuiz(CreateCustomerQuizDto createDto)
        {
            return Ok(await _customerQuizService.StartQuizAsync(createDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerQuiz(int id, UpdateCustomerQuizDto updateDto)
        {
            await _customerQuizService.UpdateCustomerQuizAsync(id, updateDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerQuiz(int id)
        {
            await _customerQuizService.DeleteCustomerQuizAsync(id);
            return Ok();
        }
    }
}
