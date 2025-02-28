using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class NewsMappingProfile : Profile
    {
        public NewsMappingProfile()
        {
            CreateMap<News, NewsDto>()
                .ForMember(dest => dest.PublisherFullName, opt => opt.MapFrom(src => src.Publisher.FullName));

            CreateMap<CreateNewsDto, News>()
                .ForMember(dest => dest.NewsId, opt => opt.Ignore())
                .ForMember(dest => dest.PublishedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsPublished, opt => opt.Ignore());

            CreateMap<UpdateNewsDto, News>()
                .ForMember(dest => dest.NewsId, opt => opt.Ignore())
                .ForMember(dest => dest.PublishedAt, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
