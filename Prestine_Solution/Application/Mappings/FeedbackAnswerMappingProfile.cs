using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class FeedbackAnswerMappingProfile : Profile
    {
        public FeedbackAnswerMappingProfile()
        {
            // Map from entity to DTO
            CreateMap<FeedbackAnswer, FeedbackAnswerDto>();

            // Map from FeedbackQuestion to simplified DTO to avoid circular references
            CreateMap<FeedbackQuestion, FeedbackQuestionBasicDto>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId));

            // Map from create DTO to entity
            CreateMap<CreateFeedbackAnswerDto, FeedbackAnswer>()
                .ForMember(dest => dest.AnswerOptionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FeedbackQuestion, opt => opt.Ignore());

            // Map from update DTO to entity
            CreateMap<UpdateFeedbackAnswerDto, FeedbackAnswer>()
                .ForMember(dest => dest.AnswerOptionId, opt => opt.Ignore())
                .ForMember(dest => dest.FeedbackQuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FeedbackQuestion, opt => opt.Ignore())
                .ForMember(dest => dest.AnswerText, opt => opt.Condition(src => src.AnswerText != null));
        }
    }
}