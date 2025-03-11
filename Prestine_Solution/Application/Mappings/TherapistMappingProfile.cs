using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class TherapistMappingProfile : Profile
    {
        public TherapistMappingProfile()
        {
            CreateMap<Therapist, TherapistDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<CreateTherapistDto, Therapist>()
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistAvailabilities, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistProfile, opt => opt.Ignore());

            CreateMap<CreateTherapistUserDto, Therapist>()
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistAvailabilities, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistProfile, opt => opt.Ignore());

            CreateMap<UpdateTherapistDto, Therapist>()
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistAvailabilities, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistProfile, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
