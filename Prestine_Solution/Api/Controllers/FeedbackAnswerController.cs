using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackAnswerController : ControllerBase
    {
        private readonly IFeedbackAnswerService _answerService;

        public FeedbackAnswerController(IFeedbackAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackAnswerDto>>> GetAllAnswers()
        {
            var answers = await _answerService.GetAllAnswersAsync();
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackAnswerDto>> GetAnswerById(int id)
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            return Ok(answer);
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<IEnumerable<FeedbackAnswerDto>>> GetAnswersByQuestionId(int questionId)
        {
            var answers = await _answerService.GetAnswersByQuestionIdAsync(questionId);
            return Ok(answers);
        }

        [HttpGet("question/{questionId}/count")]
        public async Task<ActionResult<int>> CountAnswersByQuestionId(int questionId)
        {
            var count = await _answerService.CountAnswersByQuestionIdAsync(questionId);
            return Ok(count);
        }

        [HttpPost]
        public async Task<ActionResult<FeedbackAnswerDto>> CreateAnswer(CreateFeedbackAnswerDto createDto)
        {
            var createdAnswer = await _answerService.CreateAnswerAsync(createDto);
            return CreatedAtAction(nameof(GetAnswerById), new { id = createdAnswer.AnswerOptionId }, createdAnswer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnswer(int id, UpdateFeedbackAnswerDto updateDto)
        {
            await _answerService.UpdateAnswerAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnswer(int id)
        {
            await _answerService.DeleteAnswerAsync(id);
            return NoContent();
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> AnswerExists(int id)
        {
            var exists = await _answerService.AnswerExistsAsync(id);
            return Ok(exists);
        }
    }
}