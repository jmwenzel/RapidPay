using MediatR;
using RapidPay.Models.Dto;
using System;

namespace RapidPay.App.Cqs.Commands.Card
{
    public class CreateCardAsyncCmd : IRequest<CardDto>
    {
        public CreateCardAsyncCmd(CardDto cardDto)
            => CardDto = cardDto ?? throw new ArgumentNullException(nameof(cardDto));

        public CardDto CardDto { get; set; }
    }
}
