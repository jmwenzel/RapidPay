using Microsoft.EntityFrameworkCore;
using RapidPay.Data.Models;
using RapidPay.Models.Entities;
using System.Threading.Tasks;

namespace RapidPay.Data.Repositories
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<Card> GetCardByNumber(string number)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Number == number); 
        }
    }
}
