using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // Get All - Without References
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetAllRecommendations()
        {
            return Ok(await _service.GetAllRecommendationsAsync(false));
        }

        // Get All - With References
        [HttpGet("with-reference")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetAllRecommendationsWithReferences()
        {
            return Ok(await _service.GetAllRecommendationsAsync(true));
        }

        // Get By ID - Without References
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizRecommendationDto>> GetRecommendationById(int id)
        {
            return Ok(await _service.GetRecommendationByIdAsync(id, false));
        }

        // Get By ID - With References
        [HttpGet("{id}/with-reference")]
        public async Task<ActionResult<QuizRecommendationDto>> GetRecommendationByIdWithReferences(int id)
        {
            return Ok(await _service.GetRecommendationByIdAsync(id, true));
        }

        // Get By Quiz ID - Without References
        [HttpGet("quiz/{quizId}")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetRecommendationsByQuizId(int quizId)
        {
            return Ok(await _service.GetRecommendationsByQuizIdAsync(quizId, false));
        }

        // Get By Quiz ID - With References
        [HttpGet("quiz/{quizId}/with-reference")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetRecommendationsByQuizIdWithReferences(int quizId)
        {
            return Ok(await _service.GetRecommendationsByQuizIdAsync(quizId, true));
        }

        // Get By Score - Without References
        [HttpGet("quiz/{quizId}/score/{score}")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetRecommendationsByScore(int quizId, int score)
        {
            return Ok(await _service.GetRecommendationsByScoreAsync(quizId, score, false));
        }

        // Get By Score - With References
        [HttpGet("quiz/{quizId}/score/{score}/with-reference")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetRecommendationsByScoreWithReferences(int quizId, int score)
        {
            return Ok(await _service.GetRecommendationsByScoreAsync(quizId, score, true));
        }

        // Get By Service ID - Without References
        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetRecommendationsByServiceId(int serviceId)
        {
            return Ok(await _service.GetRecommendationsByServiceIdAsync(serviceId, false));
        }

        // Get By Service ID - With References
        [HttpGet("service/{serviceId}/with-reference")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetRecommendationsByServiceIdWithReferences(int serviceId)
        {
            return Ok(await _service.GetRecommendationsByServiceIdAsync(serviceId, true));
        }

        // Get Active Recommendations - Without References
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetActiveRecommendations()
        {
            return Ok(await _service.GetActiveRecommendationsAsync(false));
        }

        // Get Active Recommendations - With References
        [HttpGet("active/with-reference")]
        public async Task<ActionResult<IEnumerable<QuizRecommendationDto>>> GetActiveRecommendationsWithReferences()
        {
            return Ok(await _service.GetActiveRecommendationsAsync(true));
        }

        // Create Recommendation
        [HttpPost]
        public async Task<ActionResult<QuizRecommendationDto>> CreateRecommendation(CreateQuizRecommendationDto createDto)
        {
            return Ok(await _service.CreateRecommendationAsync(createDto));
        }

        // Update Recommendation
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecommendation(int id, UpdateQuizRecommendationDto updateDto)
        {
            await _service.UpdateRecommendationAsync(id, updateDto);
            return NoContent();
        }

        // Delete Recommendation
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecommendation(int id)
        {
            await _service.DeleteRecommendationAsync(id);
            return NoContent();
        }
    }
}
    