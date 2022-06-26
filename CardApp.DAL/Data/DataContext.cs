using CardApp.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.DAL.Data
{
    public class DataContext : IdentityDbContext<AppUser,AppUserRole,long>
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Card>()
                .HasOne<AppUser>(c => c.Owner)
                .WithMany(u => u.Cards)
                .HasForeignKey(c => c.OwnerId);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Cards)
                .WithOne(e => e.Category)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
            var user = new AppUser
            {
                Id = 1,
                Email = "frankofoedu@gmail.com",
                EmailConfirmed = true,
                UserName = "frankofoedu@gmail.com",
                NormalizedUserName = "FRANKOFOEDU@GMAIL.COM"
            };
            var category = new Category
            {
                Id = 1,
                Name = "Food",
                Cards = new List<Card>(),
                OwnerId = 1
            };
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            user.PasswordHash = ph.HashPassword(user, "HelloWorld!");
            modelBuilder.Entity<AppUser>().HasData(user
            );
            modelBuilder.Entity<Category>().HasData(category);
        }
    }
}
