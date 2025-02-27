using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class WorkingDayMappingProfile : Profile
    {
        public WorkingDayMappingProfile()
        {
            CreateMap<WorkingDay, WorkingDayDto>()
                .ForMember(dest => dest.TimeSlotIds, opt => opt.MapFrom(src => src.TimeSlots.Select(ts => ts.SlotId).ToList()));

            CreateMap<CreateWorkingDayDto, WorkingDay>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.TimeSlots, opt => opt.Ignore());

            CreateMap<UpdateWorkingDayDto, WorkingDay>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.Ignore())
                .ForMember(dest => dest.DayName, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.TimeSlots, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
