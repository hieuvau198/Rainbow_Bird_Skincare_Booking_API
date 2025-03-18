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
        private readonly IGenericRepository<BlogComment> _repository;
        private readonly IGenericRepository<Blog> _blogRepository;
        private readonly IMapper _mapper;

        public BlogCommentService(
            IGenericRepository<BlogComment> repository,
            IGenericRepository<Blog> blogRepository,
            IMapper mapper)
        {
            _repository = repository;
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<BlogCommentDto> GetCommentByIdAsync(int commentId)
        {
            var comment = await _repository.GetByIdAsync(commentId, c => c.InverseParentComment);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            var dto = _mapper.Map<BlogCommentDto>(comment);
            dto.Replies = _mapper.Map<List<ShortBlogCommentDto>>(comment.InverseParentComment);
            return dto;
        }

        public async Task<IEnumerable<BlogCommentDto>> GetAllCommentsForBlogAsync(int blogId)
        {
            // Get only top-level comments (no parent)
            var comments = await _repository.GetAllAsync(
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
            var query = _repository.GetAllAsQueryable();

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
            // Validate blog exists
            var blog = await _blogRepository.GetByIdAsync(dto.BlogId);
            if (blog == null)
                throw new KeyNotFoundException($"Blog not found with ID: {dto.BlogId}");

            // Validate parent comment if provided
            if (dto.ParentCommentId.HasValue)
            {
                var parentComment = await _repository.GetByIdAsync(dto.ParentCommentId.Value);
                if (parentComment == null)
                    throw new KeyNotFoundException($"Parent comment not found with ID: {dto.ParentCommentId}");

                // Ensure parent comment belongs to the same blog
                if (parentComment.BlogId != dto.BlogId)
                    throw new ArgumentException("Parent comment does not belong to the specified blog");
            }

            var comment = _mapper.Map<BlogComment>(dto);
            comment.CreatedAt = DateTime.UtcNow;
            comment.Upvotes = 0;
            comment.Downvotes = 0;
            comment.IsEdited = false;
            comment.AvatarUrl = "https://img.freepik.com/premium-vector/avatar-icon0002_750950-43.jpg?semt=ais_hybrid"; // Default avatar

            await _repository.CreateAsync(comment);
            return _mapper.Map<BlogCommentDto>(comment);
        }

        public async Task<BlogCommentDto> UpdateCommentAsync(int commentId, UpdateBlogCommentDto dto)
        {
            var comment = await _repository.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            // Update comment content
            comment.Content = dto.Content;
            comment.IsEdited = true;
            comment.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(comment);
            return _mapper.Map<BlogCommentDto>(comment);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _repository.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            await _repository.DeleteAsync(comment);
        }

        public async Task<BlogCommentDto> UpvoteCommentAsync(int commentId)
        {
            var comment = await _repository.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            comment.Upvotes = (comment.Upvotes ?? 0) + 1;
            await _repository.UpdateAsync(comment);
            return _mapper.Map<BlogCommentDto>(comment);
        }

        public async Task<BlogCommentDto> DownvoteCommentAsync(int commentId)
        {
            var comment = await _repository.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comment not found with ID: {commentId}");

            comment.Downvotes = (comment.Downvotes ?? 0) + 1;
            await _repository.UpdateAsync(comment);
            return _mapper.Map<BlogCommentDto>(comment);
        }
    }
}