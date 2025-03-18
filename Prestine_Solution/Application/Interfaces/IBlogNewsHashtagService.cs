using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlogNewsHashtagService
    {
        // Blog hashtag methods
        Task<IEnumerable<BlogHashtagDto>> GetHashtagsByBlogIdAsync(int blogId);
        Task<BlogHashtagDto> AddHashtagToBlogAsync(CreateBlogHashtagDto dto);
        Task RemoveHashtagFromBlogAsync(int blogId, int hashtagId);

        // News hashtag methods
        Task<IEnumerable<NewsHashtagDto>> GetHashtagsByNewsIdAsync(int newsId);
        Task<NewsHashtagDto> AddHashtagToNewsAsync(CreateNewsHashtagDto dto);
        Task RemoveHashtagFromNewsAsync(int newsId, int hashtagId);
    }
}
