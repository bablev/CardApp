using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.DAL.Models
{
    public class Card
    {
        public long Id { get; set; }
        public string Word { get; set; }
        public string Translate { get; set; }
        public long OwnerId { get; set; }
        public Category Category { get; set; }
        public AppUser Owner { get; set; }
    }
}
