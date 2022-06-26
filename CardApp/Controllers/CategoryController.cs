using CardApp.BLL.Contracts;
using CardApp.BLL.Services;
using CardApp.DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardApp.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/user/{ownerId:long}/{category}")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        /// <summary>
        /// Создание новой категории.
        /// </summary>
        /// <param name="categoryDTO"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO, long ownerId)
        {
            await _categoryService.CreateCategoryAsync(categoryDTO, ownerId);
            return Created("~/api/user/{ownerId:long}/{category}/cards", new { ownerId = ownerId, category = categoryDTO.Name });
        }
        /// <summary>
        /// Удаление категории.
        /// </summary>
        /// <param name="categoryDTO"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromBody] CategoryDTO categoryDTO, long ownerId)
        {
            await _categoryService.DeleteCategoryAsync(categoryDTO, ownerId);
            return Accepted();
        }
        /// <summary>
        /// Получение всех категорий пользователя.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCategoriesByOwner(long ownerId)
        {
            var categories = await _categoryService.GetAllCategoriesByUser(ownerId);
            return Ok(categories);
        }
    }
}
