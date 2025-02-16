using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAllQuestions()
        {
            return Ok(await _questionService.GetAllQuestionsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionById(int id)
        {
            return Ok(await _questionService.GetQuestionByIdAsync(id));
        }

        [HttpGet("quiz/{quizId}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestionsByQuizId(int quizId)
        {
            return Ok(await _questionService.GetQuestionsByQuizIdAsync(quizId));
        }

        [HttpGet("quiz/{quizId}/active")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetActiveQuestionsByQuizId(int quizId)
        {
            return Ok(await _questionService.GetActiveQuestionsByQuizIdAsync(quizId));
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(CreateQuestionDto createDto)
        {
            return Ok(await _questionService.CreateQuestionAsync(createDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuestion(int id, UpdateQuestionDto updateDto)
        {
            await _questionService.UpdateQuestionAsync(id, updateDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            await _questionService.DeleteQuestionAsync(id);
            return Ok();
        }
    }
}
