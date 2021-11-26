using RapidPay.Models.Dto;
using System.Threading.Tasks;

namespace RapidPay.Service.Services
{
    public interface ICardService
    {
        Task<CardDto> Save(CardDto card);

        Task<CardDto> GetCardByNumber(string cardNumber);
    }
}
