using AutoMapper;
using RapidPay.Data.Repositories;
using RapidPay.Models.Dto;
using RapidPay.Models.Entities;
using System;
using System.Threading.Tasks;

namespace RapidPay.Service.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardService(IMapper mapper, ICardRepository cardRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        public async Task<CardDto> GetCardByNumber(string cardNumber)
        {
            var cardRecord = await _cardRepository.GetCardByNumber(cardNumber);
            if (cardRecord != null)
            {
                return _mapper.Map<CardDto>(cardRecord);
            }
            throw new ArgumentException("Card doesn't  exist");
        }

        public async Task<CardDto> Save(CardDto card)
        {
            var cardEntity = _mapper.Map<Card>(card);
            var cardRecord = await _cardRepository.GetCardByNumber(card.Number);
            if (cardRecord == null)
            {
                var cardInserted = await _cardRepository.AddAsync(cardEntity);

                return _mapper.Map<CardDto>(cardInserted);
            }
            throw new ArgumentException("Card already exists");
        }
    }
}
