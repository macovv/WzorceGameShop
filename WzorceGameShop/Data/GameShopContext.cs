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
        public DbSet<ClientGame> ClientsGames { get; set; }
        public DbSet<BillGame> BillsGames { get; set; }

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

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Category)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Studio)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.StudioId)
                .OnDelete(DeleteBehavior.SetNull);

            //

            modelBuilder.Entity<ClientGame>().HasKey(
               cg => new { cg.ClientId, cg.GameId }
            );

            modelBuilder.Entity<ClientGame>()
                .HasOne(bc => bc.Client)
                .WithMany(b => b.ClientGames)
                .HasForeignKey(bc => bc.ClientId);

            modelBuilder.Entity<ClientGame>()
                .HasOne(bc => bc.Game)
                .WithMany(b => b.ClientGames)
                .HasForeignKey(bc => bc.GameId);


            modelBuilder.Entity<BillGame>().HasKey(
               cg => new { cg.BillId, cg.GameId }
            );

            modelBuilder.Entity<BillGame>()
                .HasOne(bc => bc.Bill)
                .WithMany(b => b.BillGames)
                .HasForeignKey(bc => bc.BillId);

            modelBuilder.Entity<BillGame>()
                .HasOne(bc => bc.Game)
                .WithMany(b => b.BillGames)
                .HasForeignKey(bc => bc.GameId);
        }
    
    }
}
