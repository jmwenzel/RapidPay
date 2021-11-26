using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.App.Cqs.Commands.Card;
using RapidPay.App.Cqs.Queries.Card;
using RapidPay.App.Models;
using RapidPay.Infrastructure.Core.Common;
using RapidPay.Models.Dto;
using System;
using System.Threading.Tasks;

namespace RapidPay.App.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(Config.ROUTE_PREFFIX + "/v{version:apiVersion}/cards")]
    [Authorize]
    public class CardController : BaseController
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{cardNumber}")]
        public async Task<ApiResponse<CardDto>> Get(string cardNumber)
        {
            return await ExecuteAsync(async () =>
            {
                return await _mediator.Send(new GetCardAsyncQuery(cardNumber));
            });
        }

        [HttpPost]
        public async Task<ApiResponse<CardDto>> Post([FromBody] CardDto card)
        {
            return await ExecuteAsync(async () =>
            {
                return await _mediator.Send(new CreateCardAsyncCmd(card));
            });
        }
    }
}
