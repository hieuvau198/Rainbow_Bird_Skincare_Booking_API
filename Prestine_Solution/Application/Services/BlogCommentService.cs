using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogCommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BlogCommentDto> GetCommentByIdAsync(int commentId)
        {
            var comment = await _unitOfWork.BlogComments.GetByIdAsync(commentId, c => c.InverseParentComment);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            var dto = _mapper.Map<BlogCommentDto>(comment);
            dto.Replies = _mapper.Map<List<ShortBlogCommentDto>>(comment.InverseParentComment);
            return dto;
        }

        public async Task<IEnumerable<BlogCommentDto>> GetAllCommentsForBlogAsync(int blogId)
        {
            // Get only top-level comments (no parent)
            var comments = await _unitOfWork.BlogComments.GetAllAsync(
                c => c.BlogId == blogId && c.ParentCommentId == null,
                c => c.InverseParentComment);

            var dtos = new List<BlogCommentDto>();
            foreach (var comment in comments)
            {
                var dto = _mapper.Map<BlogCommentDto>(comment);
                dto.Replies = _mapper.Map<List<ShortBlogCommentDto>>(comment.InverseParentComment);
                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<IEnumerable<BlogCommentDto>> GetCommentsAsync(
            int? blogId = null,
            int? parentCommentId = null,
            string sortBy = "createdAt",
            string order = "desc",
            int page = 1,
            int size = 10)
        {
            var query = _unitOfWork.BlogComments.GetAllAsQueryable();

            if (blogId.HasValue)
                query = query.Where(c => c.BlogId == blogId.Value);

            if (parentCommentId.HasValue)
                query = query.Where(c => c.ParentCommentId == parentCommentId.Value);
            else
                query = query.Where(c => c.ParentCommentId == null); // Get only top-level comments by default

            query = sortBy.ToLower() switch
            {
                "createdAt" => order.ToLower() == "desc" ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt),
                "upvotes" => order.ToLower() == "desc" ? query.OrderByDescending(c => c.Upvotes) : query.OrderBy(c => c.Upvotes),
                _ => order.ToLower() == "desc" ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt)
            };

            var comments = await query
                .Skip((page - 1) * size)
                .Take(size)
                .Include(c => c.InverseParentComment)
                .ToListAsync();

            var dtos = new List<BlogCommentDto>();
            foreach (var comment in comments)
            {
                var dto = _mapper.Map<BlogCommentDto>(comment);
                dto.Replies = _mapper.Map<List<ShortBlogCommentDto>>(comment.InverseParentComment);
                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<BlogCommentDto> CreateCommentAsync(CreateBlogCommentDto dto)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(dto.BlogId);
            if (blog == null)
                throw new KeyNotFoundException($"Blog not found with ID: {dto.BlogId}");

            string fullName = "Anynomous";

            if (dto.UserId != null && dto.UserId != 0)
            {
                var user = await _unitOfWork.Users.GetByIdAsync(dto.UserId ?? 0);
                if (user == null) throw new KeyNotFoundException("User Not Found.");
                fullName = user.FullName ?? "Anynomous";
            }

            if (dto.ParentCommentId.HasValue && dto.ParentCommentId != 0)
            {
                var parentComment = await _unitOfWork.BlogComments.GetByIdAsync(dto.ParentCommentId.Value);
                if (parentComment == null)
                    throw new KeyNotFoundException($"Parent comment not found with ID: {dto.ParentCommentId}");

                if (parentComment.BlogId != dto.BlogId)
                    throw new ArgumentException("Parent comment does not belong to the specified blog");
            }
            else
            {
                dto.ParentCommentId = null;
            }

            var comment = _mapper.Map<BlogComment>(dto);
            comment.FullName = fullName;
            comment.CreatedAt = DateTime.UtcNow;
            comment.Upvotes = 0;
            comment.Downvotes = 0;
            comment.IsEdited = false;
            comment.AvatarUrl = "https://img.freepik.com/premium-vector/avatar-icon0002_750950-43.jpg?semt=ais_hybrid"; // Default avatar

            await _unitOfWork.BlogComments.CreateAsync(comment);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BlogCommentDto>(comment);
        }

        public async Task<BlogCommentDto> UpdateCommentAsync(int commentId, UpdateBlogCommentDto dto)
        {
            var comment = await _unitOfWork.BlogComments.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            // Update comment content
            comment.Content = dto.Content;
            comment.IsEdited = true;
            comment.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.BlogComments.UpdateAsync(comment);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BlogCommentDto>(comment);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _unitOfWork.BlogComments.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            await _unitOfWork.BlogComments.DeleteAsync(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<BlogCommentDto> UpvoteCommentAsync(int commentId)
        {
            var comment = await _unitOfWork.BlogComments.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            comment.Upvotes = (comment.Upvotes ?? 0) + 1;
            await _unitOfWork.BlogComments.UpdateAsync(comment);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BlogCommentDto>(comment);
        }

        public async Task<BlogCommentDto> DownvoteCommentAsync(int commentId)
        {
            var comment = await _unitOfWork.BlogComments.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            comment.Downvotes = (comment.Downvotes ?? 0) + 1;
            await _unitOfWork.BlogComments.UpdateAsync(comment);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BlogCommentDto>(comment);
        }
    }
}