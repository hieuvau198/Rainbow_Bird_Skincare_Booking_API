using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerFeedbackAnswersController : ControllerBase
    {
        private readonly ICustomerFeedbackAnswerService _answerService;

        public CustomerFeedbackAnswersController(ICustomerFeedbackAnswerService answerService)
        {
            _answerService = answerService;
        }

        // GET: api/CustomerFeedbackAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerFeedbackAnswerDto>>> GetAnswers()
        {
            var answers = await _answerService.GetAllAnswersAsync();
            return Ok(answers);
        }

        // GET: api/CustomerFeedbackAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerFeedbackAnswerDto>> GetAnswer(int id)
        {
            try
            {
                var answer = await _answerService.GetAnswerByIdAsync(id);
                return Ok(answer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/CustomerFeedbackAnswers/feedback/5
        [HttpGet("feedback/{feedbackId}")]
        public async Task<ActionResult<IEnumerable<CustomerFeedbackAnswerDto>>> GetAnswersByFeedbackId(int feedbackId)
        {
            var answers = await _answerService.GetAnswersByFeedbackIdAsync(feedbackId);
            return Ok(answers);
        }

        // POST: api/CustomerFeedbackAnswers
        [HttpPost]
        public async Task<ActionResult<CustomerFeedbackAnswerDto>> CreateAnswer(CreateCustomerFeedbackAnswerDto createDto)
        {
            var createdAnswer = await _answerService.CreateAnswerAsync(createDto);
            return CreatedAtAction(nameof(GetAnswer), new { id = createdAnswer.ResponseId }, createdAnswer);
        }

        // PUT: api/CustomerFeedbackAnswers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(int id, UpdateCustomerFeedbackAnswerDto updateDto)
        {
            if (!await _answerService.AnswerExistsAsync(id))
                return NotFound();

            try
            {
                await _answerService.UpdateAnswerAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/CustomerFeedbackAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            if (!await _answerService.AnswerExistsAsync(id))
                return NotFound();

            try
            {
                await _answerService.DeleteAnswerAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
