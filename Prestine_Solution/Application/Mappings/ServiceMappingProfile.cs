using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Service, ServiceDto>();

            CreateMap<CreateServiceDto, Service>()
                .ForMember(dest => dest.ServiceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceImage, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.QuizRecommendations, opt => opt.Ignore());

            CreateMap<UpdateServiceDto, Service>()
                .ForMember(dest => dest.ServiceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceImage, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.QuizRecommendations, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
