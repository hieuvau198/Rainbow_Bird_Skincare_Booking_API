using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetAllNews([FromQuery] int? hashtagId = null)
        {
            var news = await _newsService.GetAllNewsAsync(hashtagId);
            return Ok(news);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDto>> GetNews(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult<NewsDto>> CreateNews(CreateNewsDto createNewsDto)
        {
            var news = await _newsService.CreateNewsAsync(createNewsDto);
            return CreatedAtAction(nameof(GetNews), new { id = news.NewsId }, news);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, UpdateNewsDto updateNewsDto)
        {
            await _newsService.UpdateNewsAsync(id, updateNewsDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            await _newsService.DeleteNewsAsync(id);
            return NoContent();
        }
    }
}
