using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackQuestionController : ControllerBase
    {
        private readonly IFeedbackQuestionService _questionService;

        public FeedbackQuestionController(IFeedbackQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackQuestionDto>>> GetAllQuestions()
        {
            var questions = await _questionService.GetAllQuestionsAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackQuestionDto>> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            return Ok(question);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FeedbackQuestionDto>>> SearchQuestions([FromQuery] string searchTerm)
        {
            var questions = await _questionService.SearchQuestionsAsync(searchTerm);
            return Ok(questions);
        }

        [HttpPost]
        public async Task<ActionResult<FeedbackQuestionDto>> CreateQuestion(CreateFeedbackQuestionDto createDto)
        {
            var createdQuestion = await _questionService.CreateQuestionAsync(createDto);
            return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.QuestionId }, createdQuestion);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuestion(int id, UpdateFeedbackQuestionDto updateDto)
        {
            await _questionService.UpdateQuestionAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            await _questionService.DeleteQuestionAsync(id);
            return NoContent();
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> QuestionExists(int id)
        {
            var exists = await _questionService.QuestionExistsAsync(id);
            return Ok(exists);
        }
    }
}
