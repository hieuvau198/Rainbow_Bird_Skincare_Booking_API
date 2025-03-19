using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
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

            CreateMap<Transaction, TransactionDto>();
        }
    }
}
