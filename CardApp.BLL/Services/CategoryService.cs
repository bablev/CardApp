using AutoMapper;
using CardApp.BLL.Contracts;
using CardApp.DAL.Contracts;
using CardApp.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, UserManager<AppUser> userManager,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            _mapper = mapper; 
        }
        public async Task CreateCategoryAsync(CategoryDTO categoryDTO, long ownerId)
        {
            var user = await _userManager.FindByIdAsync(ownerId.ToString());
            if (user == null)
            {
                // Handling exception
            }
            var category = _mapper.Map<Category>(categoryDTO);
            category.Owner = user;
            await _categoryRepository.InsertAsync(category);
        }
        public async Task DeleteCategoryAsync(CategoryDTO categoryDTO, long ownerId)
        {
            var user = await _userManager.FindByIdAsync(ownerId.ToString());
            if (user == null)
            {
                // Handling exception
            }
            var category = _mapper.Map<Category>(categoryDTO);
            category.Owner = user;
            await _categoryRepository.RemoveAsync(category);
        }
        public async Task<List<CategoryDTO>> GetAllCategoriesByUser(long ownerId)
        {
            var user = await _userManager.FindByIdAsync(ownerId.ToString());
            if (user == null)
            {
                // Handling exception
            }
            var categories = _categoryRepository.GetAllByOwner(ownerId);
            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
            return categoriesDTO;
        }
    }
}
