using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CancelPolicyMappingProfile : Profile
    {
        public CancelPolicyMappingProfile()
        {
            CreateMap<CancelPolicy, CancelPolicyDto>();

            CreateMap<CreateCancelPolicyDto, CancelPolicy>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdateCancelPolicyDto, CancelPolicy>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
