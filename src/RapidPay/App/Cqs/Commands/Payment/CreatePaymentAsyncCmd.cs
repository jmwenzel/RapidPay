using MediatR;
using RapidPay.Models.Dto;
using System;

namespace RapidPay.App.Cqs.Commands.Payment
{
    public class CreatePaymentAsyncCmd : IRequest<PaymentFeeDto>
    {
        public CreatePaymentAsyncCmd(PaymentDto paymentDto)
            => PaymentDto = paymentDto ?? throw new ArgumentNullException(nameof(paymentDto));

        public PaymentDto PaymentDto { get; set; }
    }
}
