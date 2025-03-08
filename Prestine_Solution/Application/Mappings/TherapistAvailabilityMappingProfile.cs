using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class TherapistAvailabilityMappingProfile : Profile
    {
        public TherapistAvailabilityMappingProfile()
        {
            CreateMap<TherapistAvailability, TherapistAvailabilityDto>()
                .ForMember(dest => dest.TherapistName, opt =>
                    opt.MapFrom(src => src.Therapist.User.FullName));

            CreateMap<CreateTherapistAvailabilityDto, TherapistAvailability>()
                .ForMember(dest => dest.AvailabilityId, opt => opt.Ignore())
                .ForMember(dest => dest.Therapist, opt => opt.Ignore())
                .ForMember(dest => dest.Slot, opt => opt.Ignore());

            CreateMap<UpdateTherapistAvailabilityDto, TherapistAvailability>()
                .ForMember(dest => dest.AvailabilityId, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Therapist, opt => opt.Ignore())
                .ForMember(dest => dest.Slot, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
