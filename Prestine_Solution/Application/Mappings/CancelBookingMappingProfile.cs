using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CancelBookingMappingProfile : Profile
    {
        public CancelBookingMappingProfile()
        {
            CreateMap<CancelBooking, CancelBookingDto>();

            CreateMap<CreateCancelBookingDto, CancelBooking>()
                .ForMember(dest => dest.CancellationId, opt => opt.Ignore())
                .ForMember(dest => dest.CancelledAt, opt => opt.Ignore())
                .ForMember(dest => dest.Booking, opt => opt.Ignore());

            CreateMap<UpdateCancelBookingDto, CancelBooking>()
                .ForMember(dest => dest.CancellationId, opt => opt.Ignore())
                .ForMember(dest => dest.BookingId, opt => opt.Ignore())
                .ForMember(dest => dest.CancelledAt, opt => opt.Ignore())
                .ForMember(dest => dest.Booking, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
