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
    public class BlogNewsHashtagService : IBlogNewsHashtagService
    {
        private readonly IGenericRepository<BlogHashtag> _blogHashtagRepository;
        private readonly IGenericRepository<NewsHashtag> _newsHashtagRepository;
        private readonly IGenericRepository<Blog> _blogRepository;
        private readonly IGenericRepository<News> _newsRepository;
        private readonly IMapper _mapper;

        public BlogNewsHashtagService(
            IGenericRepository<BlogHashtag> blogHashtagRepository,
            IGenericRepository<NewsHashtag> newsHashtagRepository,
            IGenericRepository<Blog> blogRepository,
            IGenericRepository<News> newsRepository,
            IMapper mapper)
        {
            _blogHashtagRepository = blogHashtagRepository;
            _newsHashtagRepository = newsHashtagRepository;
            _blogRepository = blogRepository;
            _newsRepository = newsRepository;
            _mapper = mapper;
        }

        // Blog hashtag methods
        public async Task<IEnumerable<BlogHashtagDto>> GetHashtagsByBlogIdAsync(int blogId)
        {
            if (!await _blogRepository.ExistsAsync(b => b.BlogId == blogId))
                throw new KeyNotFoundException($"Blog not found with ID: {blogId}");

            var blogHashtags = await _blogHashtagRepository.GetAllAsync(
                bh => bh.BlogId == blogId, bh => bh.Hashtag);

            return _mapper.Map<IEnumerable<BlogHashtagDto>>(blogHashtags);
        }

        public async Task<BlogHashtagDto> AddHashtagToBlogAsync(CreateBlogHashtagDto dto)
        {
            // Check if blog exists
            if (!await _blogRepository.ExistsAsync(b => b.BlogId == dto.BlogId))
                throw new KeyNotFoundException($"Blog not found with ID: {dto.BlogId}");

            // Check if relation already exists
            if (await _blogHashtagRepository.ExistsAsync(bh => bh.BlogId == dto.BlogId && bh.HashtagId == dto.HashtagId))
                throw new InvalidOperationException("This hashtag is already added to the blog");

            var blogHashtag = _mapper.Map<BlogHashtag>(dto);
            await _blogHashtagRepository.CreateAsync(blogHashtag);

            return _mapper.Map<BlogHashtagDto>(blogHashtag);
        }

        public async Task RemoveHashtagFromBlogAsync(int blogId, int hashtagId)
        {
            var blogHashtag = await _blogHashtagRepository.FindAsync(
                bh => bh.BlogId == blogId && bh.HashtagId == hashtagId);

            if (blogHashtag == null)
                throw new KeyNotFoundException($"Hashtag with ID {hashtagId} not found for Blog with ID {blogId}");

            await _blogHashtagRepository.DeleteAsync(blogHashtag);
        }

        // News hashtag methods
        public async Task<IEnumerable<NewsHashtagDto>> GetHashtagsByNewsIdAsync(int newsId)
        {
            if (!await _newsRepository.ExistsAsync(n => n.NewsId == newsId))
                throw new KeyNotFoundException($"News not found with ID: {newsId}");

            var newsHashtags = await _newsHashtagRepository.GetAllAsync(
                nh => nh.NewsId == newsId, nh => nh.Hashtag);

            return _mapper.Map<IEnumerable<NewsHashtagDto>>(newsHashtags);
        }

        public async Task<NewsHashtagDto> AddHashtagToNewsAsync(CreateNewsHashtagDto dto)
        {
            // Check if news exists
            if (!await _newsRepository.ExistsAsync(n => n.NewsId == dto.NewsId))
                throw new KeyNotFoundException($"News not found with ID: {dto.NewsId}");

            // Check if relation already exists
            if (await _newsHashtagRepository.ExistsAsync(nh => nh.NewsId == dto.NewsId && nh.HashtagId == dto.HashtagId))
                throw new InvalidOperationException("This hashtag is already added to the news");

            var newsHashtag = _mapper.Map<NewsHashtag>(dto);
            await _newsHashtagRepository.CreateAsync(newsHashtag);

            return _mapper.Map<NewsHashtagDto>(newsHashtag);
        }

        public async Task RemoveHashtagFromNewsAsync(int newsId, int hashtagId)
        {
            var newsHashtag = await _newsHashtagRepository.FindAsync(
                nh => nh.NewsId == newsId && nh.HashtagId == hashtagId);

            if (newsHashtag == null)
                throw new KeyNotFoundException($"Hashtag with ID {hashtagId} not found for News with ID {newsId}");

            await _newsHashtagRepository.DeleteAsync(newsHashtag);
        }
    }
}
