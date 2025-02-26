using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizRecommendationsController : ControllerBase
    {
        private readonly IQuizRecommendationService _service;

        public QuizRecommendationsController(IQuizRecommendationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetAllRecommendations()
        {
            return Ok(await _service.GetAllRecommendationsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizRecommendationDto>> GetRecommendationById(int id)
        {
            return Ok(await _service.GetRecommendationByIdAsync(id));
        }

        [HttpGet("quiz/{quizId}")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetRecommendationsByQuizId(int quizId)
        {
            return Ok(await _service.GetRecommendationsByQuizIdAsync(quizId));
        }

        [HttpGet("quiz/{quizId}/score/{score}")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetRecommendationsByScore(int quizId, int score)
        {
            return Ok(await _service.GetRecommendationsByScoreAsync(quizId, score));
        }

        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetRecommendationsByServiceId(int serviceId)
        {
            return Ok(await _service.GetRecommendationsByServiceIdAsync(serviceId));
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetActiveRecommendations()
        {
            return Ok(await _service.GetActiveRecommendationsAsync());
        }

        [HttpPost]
        public async Task<ActionResult<QuizRecommendationDto>> CreateRecommendation(CreateQuizRecommendationDto createDto)
        {
            return Ok(await _service.CreateRecommendationAsync(createDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecommendation(int id, UpdateQuizRecommendationDto updateDto)
        {
            await _service.UpdateRecommendationAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecommendation(int id)
        {
            await _service.DeleteRecommendationAsync(id);
            return NoContent();
        }
    }
}
