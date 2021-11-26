using MediatR;
using RapidPay.Models.Dto;
using RapidPay.Service.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RapidPay.App.Cqs.Commands.Card
{
    public class CreateCardAsyncCmdHandler : IRequestHandler<CreateCardAsyncCmd, CardDto>
    {
        private readonly ICardService _cardService;
        public CreateCardAsyncCmdHandler(ICardService cardService)
        {
            _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        }

        public async Task<CardDto> Handle(CreateCardAsyncCmd request, CancellationToken cancellationToken)
        {
            removeEmpty(request.CardDto);
            if (request.CardDto.Number.Length == 15)
            {
                var cardDto = await _cardService.Save(request.CardDto);

                return cardDto;
            }
            throw new ArgumentException("Card number is not valid");
        }

        private void removeEmpty (CardDto card)
        {
            card.Name = card.Name.Trim();
            card.Number = card.Number.Trim();
        }
    }
}
