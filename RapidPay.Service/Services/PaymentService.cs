using AutoMapper;
using RapidPay.Data.Repositories;
using RapidPay.Models.Dto;
using RapidPay.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IUniversalFeeExchange _universalFeeExchange;
        private readonly ICardRepository _cardRepository;

        public PaymentService(IMapper mapper, IUniversalFeeExchange universalFeeExchange, ICardRepository cardRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _universalFeeExchange = universalFeeExchange ?? throw new ArgumentNullException(nameof(universalFeeExchange));
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        public async Task<PaymentFeeDto> Save(PaymentDto payment)
        {
            var card = await _cardRepository.GetCardByNumber(payment.CardNumber);
            if (card != null)
            {
                var paymentEntity = _mapper.Map<Payment>(payment);
                paymentEntity.Card = card;
                paymentEntity.Fee = _universalFeeExchange.Fee;
                paymentEntity.TotalAmount = paymentEntity.Amount - paymentEntity.Fee;

                card.Balance += paymentEntity.TotalAmount;
                card.Payments = new List<Payment>
                {
                    paymentEntity
                };

                await _cardRepository.UpdateAsync(card);
                var paymentFee = _mapper.Map<PaymentFeeDto>(paymentEntity);

                return paymentFee;
            }
            throw new ArgumentException("Card does not exist");
        }
    }
}
