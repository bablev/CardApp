using CardApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.DAL.Contracts
{
    public interface ICategoryRepository
    {
        Task InsertAsync(Category category);
        Task RemoveAsync(Category category);
        List<Category> GetAllByOwner(long id);
        Category GetCategoryByName(string categoryName);

    }
}
