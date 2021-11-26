using RapidPay.Models.Entities;
using System.Threading.Tasks;

namespace RapidPay.Data.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<Card> GetCardByNumber(string number);
    }
}
