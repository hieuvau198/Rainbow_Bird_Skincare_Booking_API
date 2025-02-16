using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Existing mappings...
            CreateMap<TherapistProfile, TherapistProfileDto>();
            CreateMap<CreateTherapistProfileDto, TherapistProfile>()
                .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore());

            CreateMap<UpdateTherapistProfileDto, TherapistProfile>()
                .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Service, ServiceDto>();
            CreateMap<CreateServiceDto, Service>()
                .ForMember(dest => dest.ServiceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceImage, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.QuizRecommendations, opt => opt.Ignore());

            CreateMap<UpdateServiceDto, Service>()
                .ForMember(dest => dest.ServiceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceImage, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.QuizRecommendations, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // TherapistAvailability mappings
            CreateMap<TherapistAvailability, TherapistAvailabilityDto>();

            CreateMap<CreateTherapistAvailabilityDto, TherapistAvailability>()
                .ForMember(dest => dest.AvailabilityId, opt => opt.Ignore())
                .ForMember(dest => dest.Therapist, opt => opt.Ignore())
                .ForMember(dest => dest.Slot, opt => opt.Ignore());

            CreateMap<UpdateTherapistAvailabilityDto, TherapistAvailability>()
                .ForMember(dest => dest.AvailabilityId, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Therapist, opt => opt.Ignore())
                .ForMember(dest => dest.Slot, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // TimeSlot mappings
            CreateMap<TimeSlot, TimeSlotDto>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.MapFrom(src => src.WorkingDayId)); // Map WorkingDayId directly
                                                                                                    // No mapping for WorkingDay reference anymore

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

            // WorkingDay mappings
            CreateMap<WorkingDay, WorkingDayDto>()
                .ForMember(dest => dest.TimeSlotIds, opt => opt.MapFrom(src => src.TimeSlots.Select(ts => ts.SlotId).ToList())); // Map TimeSlot IDs only

            CreateMap<CreateWorkingDayDto, WorkingDay>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.TimeSlots, opt => opt.Ignore()); // Do not map TimeSlots during creation

            CreateMap<UpdateWorkingDayDto, WorkingDay>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.Ignore())
                .ForMember(dest => dest.DayName, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.TimeSlots, opt => opt.Ignore()) // Do not map TimeSlots during update
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // Quiz mappings
            CreateMap<Quiz, QuizDto>();

            CreateMap<CreateQuizDto, Quiz>()
                .ForMember(dest => dest.QuizId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerQuizzes, opt => opt.Ignore())
                .ForMember(dest => dest.Questions, opt => opt.Ignore())
                .ForMember(dest => dest.QuizRecommendations, opt => opt.Ignore());

            CreateMap<UpdateQuizDto, Quiz>()
                .ForMember(dest => dest.QuizId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerQuizzes, opt => opt.Ignore())
                .ForMember(dest => dest.Questions, opt => opt.Ignore())
                .ForMember(dest => dest.QuizRecommendations, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // Question mappings
            CreateMap<Question, QuestionDto>();

            CreateMap<CreateQuestionDto, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Answers, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.Quiz, opt => opt.Ignore());

            CreateMap<UpdateQuestionDto, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.QuizId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Answers, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.Quiz, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // CustomerQuiz mappings
            CreateMap<CustomerQuiz, CustomerQuizDto>();

            CreateMap<CreateCustomerQuizDto, CustomerQuiz>()
                .ForMember(dest => dest.CustomerQuizId, opt => opt.Ignore())
                .ForMember(dest => dest.TotalScore, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.StartedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.Quiz, opt => opt.Ignore());

            CreateMap<UpdateCustomerQuizDto, CustomerQuiz>()
                .ForMember(dest => dest.CustomerQuizId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.QuizId, opt => opt.Ignore())
                .ForMember(dest => dest.StartedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.Quiz, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}