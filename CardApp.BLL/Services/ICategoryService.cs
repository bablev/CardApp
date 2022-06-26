using CardApp.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Services
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryDTO categoryDTO, long ownerId);
        Task DeleteCategoryAsync(CategoryDTO categoryDTO, long ownerId);
        Task<List<CategoryDTO>> GetAllCategoriesByUser(long ownerId);
    }
}
