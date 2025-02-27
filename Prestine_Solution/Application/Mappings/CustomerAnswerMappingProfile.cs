using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CustomerAnswerMappingProfile : Profile
    {
        public CustomerAnswerMappingProfile()
        {
            CreateMap<CustomerAnswer, CustomerAnswerDto>();

            CreateMap<CreateCustomerAnswerDto, CustomerAnswer>()
                .ForMember(dest => dest.CustomerAnswerId, opt => opt.Ignore())
                .ForMember(dest => dest.AnsweredAt, opt => opt.Ignore())
                .ForMember(dest => dest.Answer, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerQuiz, opt => opt.Ignore())
                .ForMember(dest => dest.Question, opt => opt.Ignore());

            CreateMap<UpdateCustomerAnswerDto, CustomerAnswer>()
                .ForMember(dest => dest.CustomerAnswerId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerQuizId, opt => opt.Ignore())
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.AnswerId, opt => opt.Ignore())
                .ForMember(dest => dest.AnsweredAt, opt => opt.Ignore())
                .ForMember(dest => dest.Answer, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerQuiz, opt => opt.Ignore())
                .ForMember(dest => dest.Question, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
