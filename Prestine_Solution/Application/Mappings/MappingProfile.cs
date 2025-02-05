using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to DTO
            CreateMap<TherapistProfile, TherapistProfileDto>();

            // CreateDTO to Entity
            CreateMap<CreateTherapistProfileDto, TherapistProfile>()
                .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore()); // We handle image separately

            // UpdateDTO to Entity
            CreateMap<UpdateTherapistProfileDto, TherapistProfile>()
                .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore()) // We handle image separately
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Entity to DTO
            CreateMap<Service, ServiceDto>();

            // CreateDTO to Entity
            CreateMap<CreateServiceDto, Service>()
                .ForMember(dest => dest.ServiceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceImage, opt => opt.Ignore()) // We handle image separately
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.QuizRecommendations, opt => opt.Ignore());

            // UpdateDTO to Entity
            CreateMap<UpdateServiceDto, Service>()
                .ForMember(dest => dest.ServiceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceImage, opt => opt.Ignore()) // We handle image separately
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.QuizRecommendations, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}