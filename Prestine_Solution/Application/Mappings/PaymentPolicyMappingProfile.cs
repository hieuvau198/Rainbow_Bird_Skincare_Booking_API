using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class PaymentPolicyMappingProfile : Profile
    {
        public PaymentPolicyMappingProfile()
        {
            CreateMap<PaymentPolicy, PaymentPolicyDto>();

            CreateMap<CreatePaymentPolicyDto, PaymentPolicy>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdatePaymentPolicyDto, PaymentPolicy>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
