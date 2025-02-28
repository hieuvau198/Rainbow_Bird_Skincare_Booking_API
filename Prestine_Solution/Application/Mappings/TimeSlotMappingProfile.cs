using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class TimeSlotMappingProfile : Profile
    {
        public TimeSlotMappingProfile()
        {
            CreateMap<TimeSlot, TimeSlotDto>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.MapFrom(src => src.WorkingDayId)); // Map WorkingDayId directly

            CreateMap<CreateTimeSlotDto, TimeSlot>()
                .ForMember(dest => dest.SlotId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistAvailabilities, opt => opt.Ignore())
                .ForMember(dest => dest.WorkingDay, opt => opt.Ignore());

            CreateMap<UpdateTimeSlotDto, TimeSlot>()
                .ForMember(dest => dest.SlotId, opt => opt.Ignore())
                .ForMember(dest => dest.WorkingDayId, opt => opt.Ignore()) // Prevent updating WorkingDayId
                .ForMember(dest => dest.SlotNumber, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistAvailabilities, opt => opt.Ignore())
                .ForMember(dest => dest.WorkingDay, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
