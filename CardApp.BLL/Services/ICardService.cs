using CardApp.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Services
{
    public interface ICardService
    {
        public Task<CardDTO> CreateAsync(CardDTO cardDto, long ownerId, string category);
        public Task DeleteAsync(long CardId, long OwnerId);
        public Task<IEnumerable<CardDTO>> GetCardsByCategoryAsync(long OwnerId, string category);
    }
}
