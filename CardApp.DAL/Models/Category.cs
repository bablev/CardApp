using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.DAL.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public AppUser Owner { get; set; }
        public long OwnerId { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
