using CardApp.BLL.Contracts;
using CardApp.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardApp.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/users/{ownerId:long}/{category}/cards")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        /// <summary>
        /// Получение всех карточек определенной категории пользователя.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCardsCategoryByOwner(long ownerId, string category)
        {
            var cards = await _cardService.GetCardsByCategoryAsync(ownerId, category);
            return Ok(cards);
        }
        /// <summary>
        /// Создание карточки определенной категории.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="category"></param>
        /// <param name="cardDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCard(long ownerId, string category, [FromBody] CardDTO cardDto)
        {
            var card = await _cardService.CreateAsync(cardDto, ownerId, category);
            return CreatedAtAction(nameof(GetAllCardsCategoryByOwner), new { ownerid = card.OwnerId, category=category }, card);
        }
    }
}
