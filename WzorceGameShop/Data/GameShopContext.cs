using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WzorceGameShop.Models;

namespace WzorceGameShop.Data
{
    public class GameShopContext : DbContext
    {
        public GameShopContext(DbContextOptions<GameShopContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BasketGame> BasketsGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketGame>().HasKey(
               bc => new { bc.BasketId, bc.GameId }
            );
            modelBuilder.Entity<BasketGame>()
                .HasOne(bc => bc.Basket)
                .WithMany(b => b.BasketGames)
                .HasForeignKey(bc => bc.BasketId);
            modelBuilder.Entity<BasketGame>()
                .HasOne(bc => bc.Game)
                .WithMany(b => b.BasketGames)
                .HasForeignKey(bc => bc.GameId);
        }
    }
}
