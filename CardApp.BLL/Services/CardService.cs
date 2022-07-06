using AutoMapper;
using CardApp.BLL.Contracts;
using CardApp.BLL.Exceptions;
using CardApp.DAL.Contracts;
using CardApp.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CardService> _logger;
        private readonly ICategoryRepository _categoryRepository;
        public CardService(ICardRepository cardRepository, IMapper mapper, UserManager<AppUser> userManager,
            IConfiguration configuration, ILogger<CardService> logger, ICategoryRepository categoryRepository
            )
        {
            _logger = logger;
            _userManager = userManager;
            _cardRepository = cardRepository;
            _mapper = mapper;
            _configuration = configuration;
            _categoryRepository = categoryRepository;

        }
        public async Task<CardDTO> CreateAsync(CardDTO cardDto, long ownerId, string category)
        {
            var card = _mapper.Map<Card>(cardDto);
            var user = _userManager.FindByIdAsync(ownerId.ToString());
            var categoryObj = _categoryRepository.GetCategoryByName(category);
            if (user == null)
            {
                throw new UserNotFoundException("The owner wasn't found when creating the card");
            }
            card.OwnerId = user.Id;
            card.Category = categoryObj;
            await _cardRepository.InsertAsync(card);
            return _mapper.Map<CardDTO>(card);
        }

        public async Task DeleteAsync(long cardId, long ownerId)
        {
            var card = await _cardRepository.GetByIdAsync(cardId);
            if (card is null)
            {
                throw new CardNotFoundException("The card wasn't found when deleting it");
            }

            var owner = await _userManager.FindByIdAsync(ownerId.ToString());
            if (owner is null)
            {
                throw new UserNotFoundException("The owner wasn't found when deleting the card");
            }

            if (card.Id != owner.Id)
            {
                throw new CardNotBelongToUserException("The card doesn't belong to the specific user");
            }

            await _cardRepository.Remove(card);
        }

        public async Task<IEnumerable<CardDTO>> GetCardsByCategoryAsync(long ownerId, string category)
        {
            
            var cards = await _cardRepository.GetAllByCategoryAsync(ownerId, category);
            var owner = await _userManager.FindByIdAsync(ownerId.ToString());
            if (owner is null)
            {
                throw new UserNotFoundException("The owner wasn't found");
            }
            if (owner.Id != cards?.FirstOrDefault()?.OwnerId)
            {
                throw new CardNotBelongToUserException("The card doesn't belong to the specific user");
            }
            var cardsDto = _mapper.Map<List<CardDTO>>(cards);
            return cardsDto;
        }
    }
}
