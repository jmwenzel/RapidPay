using MediatR;
using RapidPay.Models.Dto;
using RapidPay.Service.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RapidPay.App.Cqs.Commands.Payment
{
    public class CreatePaymentAsyncCmdHandler : IRequestHandler<CreatePaymentAsyncCmd, PaymentFeeDto>
    {
        private readonly IPaymentService _paymentService;

        public CreatePaymentAsyncCmdHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        public Task<PaymentFeeDto> Handle(CreatePaymentAsyncCmd request, CancellationToken cancellationToken)
        {
            removeEmpty(request.PaymentDto);
            if (request.PaymentDto.CardNumber.Length == 15)
            {
                var paymentFeeDto = _paymentService.Save(request.PaymentDto);

                return paymentFeeDto;
            }
            throw new ArgumentException("Card number is not valid");
        }

        private void removeEmpty(PaymentDto payment)
        {
            payment.CardNumber = payment.CardNumber.Trim();
        }
    }
}