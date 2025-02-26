using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
                        
            #region User mappings
            CreateMap<User, UserDto>();

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore()) // Will be set manually with BCrypt
                .ForMember(dest => dest.Staff, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Therapist, opt => opt.Ignore());

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
                .ForMember(dest => dest.Staff, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Therapist, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region Customer mappings
            CreateMap<Customer, CustomerDto>();

            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.LastVisitAt, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerQuizzes, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<UpdateCustomerDto, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerQuizzes, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region Manager mappings
            CreateMap<Manager, ManagerDto>();

            CreateMap<CreateManagerDto, Manager>()
                .ForMember(dest => dest.ManagerId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<UpdateManagerDto, Manager>()
                .ForMember(dest => dest.ManagerId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region Staff mappings
            CreateMap<Staff, StaffDto>();

            CreateMap<CreateStaffDto, Staff>()
                .ForMember(dest => dest.StaffId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<CreateStaffUserDto, Staff>()
                .ForMember(dest => dest.StaffId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<UpdateStaffDto, Staff>()
                .ForMember(dest => dest.StaffId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region Therapist mappings
            CreateMap<Therapist, TherapistDto>();

            CreateMap<CreateTherapistDto, Therapist>()
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistAvailabilities, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistProfile, opt => opt.Ignore());

            CreateMap<CreateTherapistUserDto, Therapist>()
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistAvailabilities, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistProfile, opt => opt.Ignore());

            CreateMap<UpdateTherapistDto, Therapist>()
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistAvailabilities, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistProfile, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region TherapistProfile mappings
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

            #endregion

            #region TherapistAvailability mappings
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

            #endregion

            #region WorkingDay mappings
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
            #endregion

            #region TimeSlot mappings
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

            #endregion

            #region Service mappings
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

            #endregion

            #region Quiz mappings
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
            #endregion

            #region Question mappings
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
            #endregion

            #region CustomerQuiz mappings
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

            #endregion

            #region Answer mappings
            CreateMap<Answer, AnswerDto>();

            CreateMap<CreateAnswerDto, Answer>()
                .ForMember(dest => dest.AnswerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.Question, opt => opt.Ignore());

            CreateMap<UpdateAnswerDto, Answer>()
                .ForMember(dest => dest.AnswerId, opt => opt.Ignore())
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerAnswers, opt => opt.Ignore())
                .ForMember(dest => dest.Question, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region QuizRecommendation mappings
            CreateMap<QuizRecommendation, QuizRecommendationDto>();

            CreateMap<CreateQuizRecommendationDto, QuizRecommendation>()
                .ForMember(dest => dest.RecommendationId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Quiz, opt => opt.Ignore())
                .ForMember(dest => dest.Service, opt => opt.Ignore());

            CreateMap<UpdateQuizRecommendationDto, QuizRecommendation>()
                .ForMember(dest => dest.RecommendationId, opt => opt.Ignore())
                .ForMember(dest => dest.QuizId, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Quiz, opt => opt.Ignore())
                .ForMember(dest => dest.Service, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region CustomerAnswer mappings
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

            #endregion

            #region Payment mappings
            CreateMap<Payment, PaymentDto>();

            CreateMap<CreatePaymentDto, Payment>()
                .ForMember(dest => dest.PaymentId, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentDate, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore());

            CreateMap<UpdatePaymentDto, Payment>()
                .ForMember(dest => dest.PaymentId, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Currency, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentMethod, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region Booking mappings
            CreateMap<Booking, BookingDto>();

            CreateMap<CreateBookingDto, Booking>()
                .ForMember(dest => dest.BookingId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentId, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Payment, opt => opt.Ignore())
                .ForMember(dest => dest.Review, opt => opt.Ignore())
                .ForMember(dest => dest.Service, opt => opt.Ignore())
                .ForMember(dest => dest.Slot, opt => opt.Ignore())
                .ForMember(dest => dest.Therapist, opt => opt.Ignore())
                .ForMember(dest => dest.CancelBooking, opt => opt.Ignore());

            CreateMap<UpdateBookingDto, Booking>()
                .ForMember(dest => dest.BookingId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.TherapistId, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceId, opt => opt.Ignore())
                .ForMember(dest => dest.SlotId, opt => opt.Ignore())
                .ForMember(dest => dest.BookingDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Payment, opt => opt.Ignore())
                .ForMember(dest => dest.Review, opt => opt.Ignore())
                .ForMember(dest => dest.Service, opt => opt.Ignore())
                .ForMember(dest => dest.Slot, opt => opt.Ignore())
                .ForMember(dest => dest.Therapist, opt => opt.Ignore())
                .ForMember(dest => dest.CancelBooking, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region CancelBooking mappings
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

            #endregion

            #region CancelPolicy mappings
            CreateMap<CancelPolicy, CancelPolicyDto>();

            CreateMap<CreateCancelPolicyDto, CancelPolicy>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdateCancelPolicyDto, CancelPolicy>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            #region PaymentPolicy mappings
            CreateMap<PaymentPolicy, PaymentPolicyDto>();

            CreateMap<CreatePaymentPolicyDto, PaymentPolicy>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdatePaymentPolicyDto, PaymentPolicy>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion

            

        }
    }
}