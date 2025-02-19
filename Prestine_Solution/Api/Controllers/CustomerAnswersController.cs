
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAnswersController : ControllerBase
    {
        private readonly ICustomerAnswerService _service;

        public CustomerAnswersController(ICustomerAnswerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAnswerDto>>> GetAllCustomerAnswers()
        {
            return Ok(await _service.GetAllCustomerAnswersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAnswerDto>> GetCustomerAnswerById(int id)
        {
            return Ok(await _service.GetCustomerAnswerByIdAsync(id));
        }

        [HttpGet("quiz/{customerQuizId}")]
        public async Task<ActionResult<IEnumerable<CustomerAnswerDto>>> GetAnswersByCustomerQuizId(int customerQuizId)
        {
            return Ok(await _service.GetAnswersByCustomerQuizIdAsync(customerQuizId));
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<IEnumerable<CustomerAnswerDto>>> GetAnswersByQuestionId(int questionId)
        {
            return Ok(await _service.GetAnswersByQuestionIdAsync(questionId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerAnswerDto>> CreateCustomerAnswer(CreateCustomerAnswerDto createDto)
        {
            return Ok(await _service.CreateCustomerAnswerAsync(createDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerAnswer(int id, UpdateCustomerAnswerDto updateDto)
        {
            await _service.UpdateCustomerAnswerAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerAnswer(int id)
        {
            await _service.DeleteCustomerAnswerAsync(id);
            return NoContent();
        }
    }
}
