using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CustomerFeedbackMappingProfile : Profile
    {
        public CustomerFeedbackMappingProfile()
        {
            // Map from entity to DTO
            CreateMap<CustomerFeedback, CustomerFeedbackDto>();

            // Map from Booking to simplified BookingBasicDto
            CreateMap<Booking, BookingBasicDto>();

            // Map from CustomerFeedbackAnswer to DTO
            CreateMap<CustomerFeedbackAnswer, CustomerFeedbackAnswerDto>();

            // Map from create DTO to entity
            CreateMap<CreateCustomerFeedbackDto, CustomerFeedback>()
                .ForMember(dest => dest.CustomerFeedbackId, opt => opt.Ignore())
                .ForMember(dest => dest.SubmittedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Booking, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerFeedbackAnswers, opt => opt.Ignore());

            CreateMap<CreateCustomerFeedbackAnswerDto, CustomerFeedbackAnswer>();

            // Map from update DTO to entity
            CreateMap<UpdateCustomerFeedbackDto, CustomerFeedback>()
                .ForMember(dest => dest.CustomerFeedbackId, opt => opt.Ignore())
                .ForMember(dest => dest.BookingId, opt => opt.Ignore())
                .ForMember(dest => dest.Booking, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerFeedbackAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.SubmittedAt, opt => opt.Condition(src => src.SubmittedAt.HasValue));
        }
    }
}