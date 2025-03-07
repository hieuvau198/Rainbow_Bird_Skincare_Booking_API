using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CustomerFeedbackAnswerMappingProfile : Profile
    {
        public CustomerFeedbackAnswerMappingProfile()
        {
            // Entity to DTO
            CreateMap<CustomerFeedbackAnswer, CustomerFeedbackAnswerDto>();

            // DTO to Entity
            CreateMap<CreateCustomerFeedbackAnswerDto, CustomerFeedbackAnswer>();

            // Update DTO to Entity
            CreateMap<UpdateCustomerFeedbackAnswerDto, CustomerFeedbackAnswer>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}