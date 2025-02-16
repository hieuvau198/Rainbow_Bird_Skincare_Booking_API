using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetAllAnswers()
        {
            return Ok(await _answerService.GetAllAnswersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDto>> GetAnswerById(int id)
        {
            return Ok(await _answerService.GetAnswerByIdAsync(id));
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetAnswersByQuestionId(int questionId)
        {
            return Ok(await _answerService.GetAnswersByQuestionIdAsync(questionId));
        }

        [HttpPost]
        public async Task<ActionResult<AnswerDto>> CreateAnswer(CreateAnswerDto createDto)
        {
            return Ok(await _answerService.CreateAnswerAsync(createDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnswer(int id, UpdateAnswerDto updateDto)
        {
            await _answerService.UpdateAnswerAsync(id, updateDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnswer(int id)
        {
            await _answerService.DeleteAnswerAsync(id);
            return Ok();
        }
    }
}
