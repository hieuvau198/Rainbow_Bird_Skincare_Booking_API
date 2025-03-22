using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDto>> GetAllBlogsAsync(int? hashtagId = null)
        {
            IEnumerable<Blog> blogs;

            if (hashtagId.HasValue && hashtagId > 0)
            {
                var blogHashtags = await _unitOfWork.BlogHashtags.GetAllAsync(
                    bh => bh.HashtagId == hashtagId.Value,
                    bh => bh.Blog.Author
                );

                blogs = blogHashtags.Select(bh => bh.Blog).Distinct();
            }
            else
            {
                blogs = await _unitOfWork.Blogs.GetAllAsync(null, b => b.Author);
            }

            return _mapper.Map<IEnumerable<BlogDto>>(blogs);
        }

        public async Task<BlogDto> GetBlogByIdAsync(int blogId)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId, b => b.Author);
            if (blog == null)
                throw new KeyNotFoundException($"Blog with ID {blogId} not found");

            return _mapper.Map<BlogDto>(blog);
        }

        public async Task<BlogDto> CreateBlogAsync(CreateBlogDto createBlogDto)
        {
            var author = await _unitOfWork.Users.GetByIdAsync(createBlogDto.AuthorId);
            if (author == null)
                throw new KeyNotFoundException($"Author with ID {createBlogDto.AuthorId} not found");

            var blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Blogs.CreateAsync(blog);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BlogDto>(blog);
        }

        public async Task UpdateBlogAsync(int blogId, UpdateBlogDto updateBlogDto)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog == null)
                throw new KeyNotFoundException($"Blog with ID {blogId} not found");

            _mapper.Map(updateBlogDto, blog);
            blog.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Blogs.UpdateAsync(blog);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(int blogId)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog == null)
                throw new KeyNotFoundException($"Blog with ID {blogId} not found");

            await _unitOfWork.Blogs.DeleteAsync(blog);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}