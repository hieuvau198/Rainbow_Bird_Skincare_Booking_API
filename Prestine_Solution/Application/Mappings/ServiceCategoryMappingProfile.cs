using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ServiceCategoryMappingProfile : Profile
    {
        public ServiceCategoryMappingProfile()
        {
            CreateMap<ServiceCategory, ServiceCategoryDto>();

            CreateMap<CreateServiceCategoryDto, ServiceCategory>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.Services, opt => opt.Ignore());

            CreateMap<UpdateServiceCategoryDto, ServiceCategory>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.Services, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
