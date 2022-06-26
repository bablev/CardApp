using AutoMapper;
using CardApp.BLL.Contracts;
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
                // Handling exception
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
                // Handling exception
            }

            var owner = await _userManager.FindByIdAsync(ownerId.ToString());
            if (owner is null)
            {
                // Handling exception
            }

            if (card.Id != owner.Id)
            {
                // Handling exception
            }

            await _cardRepository.Remove(card);
        }

        public async Task<IEnumerable<CardDTO>> GetCardsByCategoryAsync(long ownerId, string category)
        {   
            var cards = await _cardRepository.GetAllByCategoryAsync(ownerId, category);
            var cardsDto = _mapper.Map<List<CardDTO>>(cards);
            return cardsDto;
        }
    }
}
