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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Hashtag.Name));
            CreateMap<CreateBlogHashtagDto, BlogHashtag>();

            // News Hashtag mappings
            CreateMap<NewsHashtag, NewsHashtagDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Hashtag.Name));
            CreateMap<CreateNewsHashtagDto, NewsHashtag>();
        }
    }
}
