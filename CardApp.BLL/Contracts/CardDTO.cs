using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Contracts
{
    public class CardDTO
    {
        [Required]
        public string Word { get; set; }
        [Required]
        public string Translate { get; set; }
        public long? OwnerId { get; set; }
    }
}
