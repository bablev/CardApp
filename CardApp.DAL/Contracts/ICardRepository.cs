using CardApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.DAL.Contracts
{
    public interface ICardRepository
    {
        Task InsertAsync(Card card);
        Task Remove(Card card);
        Task<Card?> GetByIdAsync(long id);
        Task<ICollection<Card>> GetAllByCategoryAsync(long id, string category);
    }
}
