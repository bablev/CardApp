using CardApp.DAL.Contracts;
using CardApp.DAL.Data;
using CardApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.DAL.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context) => _context = context;
        public async Task InsertAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        public List<Category> GetAllByOwner(long id)
        {
            var categories = _context.Categories.Where(c => c.Owner.Id == id).ToList();
            return categories;
        }
        public Category GetCategoryByName(string categoryName)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName);
            return category;
        }
    }
}
