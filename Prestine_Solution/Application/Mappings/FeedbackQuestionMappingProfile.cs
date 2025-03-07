using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class FeedbackQuestionMappingProfile : Profile
    {
        public FeedbackQuestionMappingProfile()
        {
            // Map from entity to DTO
            CreateMap<FeedbackQuestion, FeedbackQuestionDto>()
                .ForMember(dest => dest.FeedbackAnswers, opt => opt.ExplicitExpansion());

            // Map from create DTO to entity
            CreateMap<CreateFeedbackQuestionDto, FeedbackQuestion>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FeedbackAnswers, opt => opt.Ignore());

            // Map from update DTO to entity
            CreateMap<UpdateFeedbackQuestionDto, FeedbackQuestion>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FeedbackAnswers, opt => opt.Ignore());
        }
    }
}