using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDto>> GetAllBlogsAsync(int? hashtagId = null);
        Task<BlogDto> GetBlogByIdAsync(int blogId);
        Task<BlogDto> CreateBlogAsync(CreateBlogDto createBlogDto);
        Task UpdateBlogAsync(int blogId, UpdateBlogDto updateBlogDto);
        Task DeleteBlogAsync(int blogId);
    }
}
