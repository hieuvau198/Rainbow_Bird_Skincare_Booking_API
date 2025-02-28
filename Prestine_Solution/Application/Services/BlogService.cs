using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IGenericRepository<Blog> _repository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public BlogService(
            IGenericRepository<Blog> repository,
            IGenericRepository<User> userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDto>> GetAllBlogsAsync()
        {
            var blogs = await _repository.GetAllAsync(null, b => b.Author);
            return _mapper.Map<IEnumerable<BlogDto>>(blogs);
        }

        public async Task<BlogDto> GetBlogByIdAsync(int blogId)
        {
            var blog = await _repository.GetByIdAsync(blogId, b => b.Author);
            if (blog == null)
                throw new KeyNotFoundException($"Blog with ID {blogId} not found");

            return _mapper.Map<BlogDto>(blog);
        }

        public async Task<BlogDto> CreateBlogAsync(CreateBlogDto createBlogDto)
        {
            var author = await _userRepository.GetByIdAsync(createBlogDto.AuthorId);
            if (author == null)
                throw new KeyNotFoundException($"Author with ID {createBlogDto.AuthorId} not found");

            var blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreatedAt = DateTime.UtcNow;

            await _repository.CreateAsync(blog);
            return _mapper.Map<BlogDto>(blog);
        }

        public async Task UpdateBlogAsync(int blogId, UpdateBlogDto updateBlogDto)
        {
            var blog = await _repository.GetByIdAsync(blogId);
            if (blog == null)
                throw new KeyNotFoundException($"Blog with ID {blogId} not found");

            _mapper.Map(updateBlogDto, blog);
            blog.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(blog);
        }

        public async Task DeleteBlogAsync(int blogId)
        {
            var blog = await _repository.GetByIdAsync(blogId);
            if (blog == null)
                throw new KeyNotFoundException($"Blog with ID {blogId} not found");

            await _repository.DeleteAsync(blog);
        }
    }
}
