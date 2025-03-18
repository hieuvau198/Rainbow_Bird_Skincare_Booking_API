using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHashtagService
    {
        Task<HashtagDto> GetHashtagByIdAsync(int hashtagId);
        Task<IEnumerable<HashtagDto>> GetAllHashtagsAsync();
        Task<HashtagDto> CreateHashtagAsync(CreateHashtagDto dto);
        Task<HashtagDto> UpdateHashtagAsync(int hashtagId, UpdateHashtagDto dto);
        Task DeleteHashtagAsync(int hashtagId);
    }
}