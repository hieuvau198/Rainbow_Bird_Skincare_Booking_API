using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlogCommentService
    {
        Task<BlogCommentDto> GetCommentByIdAsync(int commentId);
        Task<IEnumerable<BlogCommentDto>> GetAllCommentsForBlogAsync(int blogId);
        Task<IEnumerable<BlogCommentDto>> GetCommentsAsync(
            int? blogId = null,
            int? parentCommentId = null,
            string sortBy = "createdAt",
            string order = "desc",
            int page = 1,
            int size = 10);
        Task<BlogCommentDto> CreateCommentAsync(CreateBlogCommentDto dto);
        Task<BlogCommentDto> UpdateCommentAsync(int commentId, UpdateBlogCommentDto dto);
        Task DeleteCommentAsync(int commentId);
        Task<BlogCommentDto> UpvoteCommentAsync(int commentId);
        Task<BlogCommentDto> DownvoteCommentAsync(int commentId);
    }
}
