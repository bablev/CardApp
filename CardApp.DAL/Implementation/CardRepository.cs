using CardApp.DAL.Contracts;
using CardApp.DAL.Data;
using CardApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.DAL.Implementation
{
    public class CardRepository : ICardRepository
    {
        private readonly DataContext _context;
        public CardRepository(DataContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(Card card)
        {
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Card card)
        {
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task<Card?> GetByIdAsync(long id)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<Card>> GetAllByCategoryAsync(long id, string category)
        {
            var cards = _context.Cards.Where(c => c.Category.Name == category).ToList();
            return cards;
        }
    }
}
