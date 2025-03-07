using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CustomerRatingMappingProfile : Profile
    {
        public CustomerRatingMappingProfile()
        {
            // Map from entity to DTO
            CreateMap<CustomerRating, CustomerRatingDto>()
                .ForMember(dest => dest.ExperienceImageUrl, opt => opt.MapFrom(src => src.ExperienceImageUrl ?? string.Empty))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment ?? string.Empty))
                // We don't map Booking here to avoid circular references, it will be handled separately
                .ForMember(dest => dest.Booking, opt => opt.ExplicitExpansion());

            // Map from create DTO to entity
            CreateMap<CreateCustomerRatingDto, CustomerRating>()
                .ForMember(dest => dest.RatingId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Booking, opt => opt.Ignore());

            // Map from update DTO to entity - handles partial updates where properties might be null
            CreateMap<UpdateCustomerRatingDto, CustomerRating>()
                .ForMember(dest => dest.RatingId, opt => opt.Ignore())
                .ForMember(dest => dest.BookingId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Booking, opt => opt.Ignore())
                .ForMember(dest => dest.RatingValue, opt => opt.Condition(src => src.RatingValue.HasValue))
                .ForMember(dest => dest.ExperienceImageUrl, opt => opt.Condition(src => src.ExperienceImageUrl != null))
                .ForMember(dest => dest.Comment, opt => opt.Condition(src => src.Comment != null));
        }
    }
}