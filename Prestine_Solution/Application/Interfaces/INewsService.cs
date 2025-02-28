using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetAllNewsAsync();
        Task<NewsDto> GetNewsByIdAsync(int newsId);
        Task<NewsDto> CreateNewsAsync(CreateNewsDto createNewsDto);
        Task UpdateNewsAsync(int newsId, UpdateNewsDto updateNewsDto);
        Task DeleteNewsAsync(int newsId);
    }
}
