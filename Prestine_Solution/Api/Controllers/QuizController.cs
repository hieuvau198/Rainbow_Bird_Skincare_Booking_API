using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetAllQuizzes()
        {
            return Ok(await _quizService.GetAllQuizzesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDto>> GetQuizById(int id)
        {
            return Ok(await _quizService.GetQuizByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<QuizDto>> CreateQuiz(CreateQuizDto createDto)
        {
            return Ok(await _quizService.CreateQuizAsync(createDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuiz(int id, UpdateQuizDto updateDto)
        {
            await _quizService.UpdateQuizAsync(id, updateDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            await _quizService.DeleteQuizAsync(id);
            return Ok();
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetActiveQuizzes()
        {
            return Ok(await _quizService.GetActiveQuizzesAsync());
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizzesByCategory(string category)
        {
            return Ok(await _quizService.GetQuizzesByCategoryAsync(category));
        }
    }
}
