using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WzorceGameShop.Data;
using WzorceGameShop.Models;

namespace WzorceGameShop.Controllers
{
    public class BillsController : Controller
    {
        private readonly GameShopContext _context;
        public BillsController(GameShopContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> GenerateBill()
        {
            var basket = await _context.Baskets.Include(x => x.BasketGames).FirstOrDefaultAsync(x => x.Id == 3);
            var client = _context.Clients.FirstOrDefault(x => x.Id == 1); // chyba na sztywno najlepiej poki co

            //var basketGames = _context.BasketsGames.FirstOrDefault(x => x.BasketId == 3);
            var bill = new Bill();
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();

            foreach (var basketGame in basket.BasketGames)
            {
                var billGame = new BillGame();
                var game = _context.Games.FirstOrDefault(x => x.Id == basketGame.GameId);
                var gameOfUser = _context.ClientsGames.Where(x => x.GameId == basketGame.GameId
                        && x.ClientId == client.Id).ToList();
                if (gameOfUser.Count != 0)
                {
                    continue; // chyba dobre bo ine doda ceny etc.
                }
                bill.SummaryPrice += game.Price;
                billGame.BillId = bill.Id;
                billGame.GameId = game.Id;
                await _context.BillsGames.AddAsync(billGame);
                await _context.SaveChangesAsync();
            }
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = bill.Id });
            //return RedirectToAction("Details", 13);
        }

        // GET: BillsController/Details/5
        public ActionResult Details(int? id)
        {
            var bill = _context.Bills.FirstOrDefault(x => x.Id == id);
            return View(bill);
        }

        public async Task<IActionResult> Buy(int? id)
        {

            // ew. żeby addtobasket było przy nich wyłączone
            // trzeba też zrobić liste z gierami dla danego użytkownika + zwiększanie rozmiaru salda
            // porpawić description
            // koszyki powinny być dynamiczne a nie tylko o id 3, ew. 1 user 1 koszyk
            // może klasa clientBasket czy coś one2one client do koszyka

            var client = _context.Clients.FirstOrDefault(x => x.Id == 1); // chyba na sztywno najlepiej poki co
            var bill = _context.Bills.FirstOrDefault(x => x.Id == id);
            var billGames = _context.BillsGames.Where(x => x.BillId == id);

            if(client.Saldo-bill.SummaryPrice >= 0)
            {
                client.Saldo -= bill.SummaryPrice;
                _context.Clients.Update(client);
            }
            else
            {
                return RedirectToAction("Index", "Games"); // na teraz, później że saldo za małe
            }

            foreach (var billGame in billGames)
            {
                var clientGame = new ClientGame();
                clientGame.ClientId = client.Id;
                clientGame.GameId = billGame.GameId;
                _context.ClientsGames.Add(clientGame);
            }

            var basket = _context.Baskets.Include(x => x.BasketGames).FirstOrDefault(x => x.Id == 3);
            basket.BasketGames = null;
            basket.SelectedGames = null;
            _context.Baskets.Update(basket);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Games"); // na teraz
        }
    }
}
