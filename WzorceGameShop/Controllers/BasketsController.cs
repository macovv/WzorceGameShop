﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WzorceGameShop.Data;
using WzorceGameShop.Models;

namespace WzorceGameShop.Controllers
{
    public class BasketsController : Controller
    {
        private readonly GameShopContext _context;

        public BasketsController(GameShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var baskets = await _context.BasketsGames
                .Where(m => m.BasketId == 3).ToListAsync();
            if (baskets == null)
            {
                return NotFound();
            }

            var gameList = new List<Game>();

            foreach(var x in baskets)
            {
                var game = await _context.Games
                    .FirstOrDefaultAsync(b => x.GameId == b.Id);
                gameList.Add(game);
            }

            //var games =
            return View(gameList);
        }

        //public async Task<IActionResult> AddToBasket(int basketId, int gameId)
        public async Task<IActionResult> AddToBasket(int? id)
        {
            //int gameId = 1;
            var basket = await _context.Baskets.Include(x => x.BasketGames).FirstOrDefaultAsync(x => x.Id == 3);
            if (basket == null)
            {
                basket = new Basket();
                await _context.Baskets.AddAsync(basket);
                await _context.SaveChangesAsync();
            }
            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == id);
            var basketGame = new BasketGame();

            if (game != null && basket.BasketGames.Count(x => x.GameId == id) == 0)
            {
                basketGame.BasketId = basket.Id;
                basketGame.GameId = game.Id;
                await _context.BasketsGames.AddAsync(basketGame);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Games");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var basket = await _context.Baskets.Include(x => x.BasketGames).FirstOrDefaultAsync(x => x.Id == 3);
            if (basket == null)
            {
                basket = new Basket();
                await _context.Baskets.AddAsync(basket);
                await _context.SaveChangesAsync();
            }

            var basketGame = basket.BasketGames.Where(x => x.GameId == id).FirstOrDefault();

            _context.BasketsGames.Remove(basketGame);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Baskets");
        }
    }
}
