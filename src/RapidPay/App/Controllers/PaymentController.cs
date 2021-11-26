using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.App.Cqs.Commands.Payment;
using RapidPay.App.Models;
using RapidPay.Infrastructure.Core.Common;
using RapidPay.Models.Dto;
using System;
using System.Threading.Tasks;

namespace RapidPay.App.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(Config.ROUTE_PREFFIX + "/v{version:apiVersion}/payments")]
    [Authorize]
    public class PaymentController : BaseController
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<ApiResponse<PaymentFeeDto>> Post([FromBody] PaymentDto payment)
        {
            return await ExecuteAsync(async () =>
            {
                return await _mediator.Send(new CreatePaymentAsyncCmd(payment));
            });
        }
    }
}
