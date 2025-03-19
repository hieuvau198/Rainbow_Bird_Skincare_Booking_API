using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly IGenericRepository<News> _repository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<NewsHashtag> _newsHashtagRepository;
        private readonly IMapper _mapper;

        public NewsService(
            IGenericRepository<News> repository,
            IGenericRepository<User> userRepository,
            IGenericRepository<NewsHashtag> newsHashtagRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _newsHashtagRepository = newsHashtagRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync(int? hashtagId = null)
        {
            IEnumerable<News> newsList;

            if (hashtagId.HasValue && hashtagId > 0)
            {
                var newsHashtags = await _newsHashtagRepository.GetAllAsync(
                    nh => nh.HashtagId == hashtagId.Value,
                    nh => nh.News.Publisher
                );

                newsList = newsHashtags.Select(nh => nh.News).Distinct();
            }
            else
            {
                newsList = await _repository.GetAllAsync(null, n => n.Publisher);
            }

            return _mapper.Map<IEnumerable<NewsDto>>(newsList);
        }

        public async Task<NewsDto> GetNewsByIdAsync(int newsId)
        {
            var news = await _repository.GetByIdAsync(newsId, n => n.Publisher);
            if (news == null)
                throw new KeyNotFoundException($"News with ID {newsId} not found");

            return _mapper.Map<NewsDto>(news);
        }

        public async Task<NewsDto> CreateNewsAsync(CreateNewsDto createNewsDto)
        {
            var publisher = await _userRepository.GetByIdAsync(createNewsDto.PublisherId);
            if (publisher == null)
                throw new KeyNotFoundException($"Publisher with ID {createNewsDto.PublisherId} not found");

            var news = _mapper.Map<News>(createNewsDto);
            news.PublishedAt = DateTime.UtcNow;
            news.IsPublished = false; // Default to unpublished

            await _repository.CreateAsync(news);
            return _mapper.Map<NewsDto>(news);
        }

        public async Task UpdateNewsAsync(int newsId, UpdateNewsDto updateNewsDto)
        {
            var news = await _repository.GetByIdAsync(newsId);
            if (news == null)
                throw new KeyNotFoundException($"News with ID {newsId} not found");

            _mapper.Map(updateNewsDto, news);
            await _repository.UpdateAsync(news);
        }

        public async Task DeleteNewsAsync(int newsId)
        {
            var news = await _repository.GetByIdAsync(newsId);
            if (news == null)
                throw new KeyNotFoundException($"News with ID {newsId} not found");

            await _repository.DeleteAsync(news);
        }
    }
}
