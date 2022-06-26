using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.DAL.Models
{
    public class AppUser : IdentityUser<long>
    {
        public ICollection<Card>? Cards { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
