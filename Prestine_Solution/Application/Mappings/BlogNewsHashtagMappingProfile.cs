using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class BlogNewsHashtagMappingProfile : Profile
    {
        public BlogNewsHashtagMappingProfile()
        {
            // Blog Hashtag mappings
            CreateMap<BlogHashtag, BlogHashtagDto>()
                .ForMember(dest => dest.Tittle, opt => opt.MapFrom(src => src.Blog.Title));
            CreateMap<CreateBlogHashtagDto, BlogHashtag>();

            // News Hashtag mappings
            CreateMap<NewsHashtag, NewsHashtagDto>()
                .ForMember(dest => dest.Tittle, opt => opt.MapFrom(src => src.News.Title));
            CreateMap<CreateNewsHashtagDto, NewsHashtag>();
        }
    }
}
