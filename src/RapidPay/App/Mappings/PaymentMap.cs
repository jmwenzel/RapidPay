using AutoMapper;
using RapidPay.Models.Dto;
using RapidPay.Models.Entities;

namespace RapidPay.App.Mappings
{
    public class PaymentMap : Profile
    {
        public PaymentMap()
        {
            CreateMap<PaymentDto, Payment>();

            CreateMap<Payment, PaymentFeeDto>();

        }
    }
}
