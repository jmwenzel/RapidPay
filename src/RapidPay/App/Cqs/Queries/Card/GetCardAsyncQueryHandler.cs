using MediatR;
using RapidPay.Models.Dto;
using RapidPay.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RapidPay.App.Cqs.Queries.Card
{
    public class GetCardAsyncQueryHandler : IRequestHandler<GetCardAsyncQuery, CardDto>
    {
        private readonly ICardService _cardService;

        public GetCardAsyncQueryHandler(ICardService cardService)
        {
            _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        }

        public Task<CardDto> Handle(GetCardAsyncQuery request, CancellationToken cancellationToken)
        {
            var cardNumber = request.CardNumber.Trim();
            if (cardNumber.Length == 15)
            {
                return _cardService.GetCardByNumber(cardNumber);
            }
            throw new ArgumentException("Card number is not valid");
        }
    }
}
