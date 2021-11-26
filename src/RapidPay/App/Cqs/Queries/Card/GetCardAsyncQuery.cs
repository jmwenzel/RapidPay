using MediatR;
using RapidPay.Models.Dto;

namespace RapidPay.App.Cqs.Queries.Card
{
    public class GetCardAsyncQuery : IRequest<CardDto>
    {
        public GetCardAsyncQuery(string cardNumber)
            => CardNumber = cardNumber;

        public string CardNumber { get; set; }
    }
}
