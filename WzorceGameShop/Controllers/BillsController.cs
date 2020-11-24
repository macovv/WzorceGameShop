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

            //var basketGames = _context.BasketsGames.FirstOrDefault(x => x.BasketId == 3);
            var bill = new Bill();
            foreach(var basketGame in basket.BasketGames)
            {
                var game = _context.Games.FirstOrDefault(x => x.Id == basketGame.GameId);
                bill.SummaryPrice += game.Price;
            }
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", 1);
        }

        // GET: BillsController/Details/5
        public ActionResult Details(int? id)
        {
            var bill = _context.Bills.FirstOrDefault(x => x.Id == 1);
            return View(bill);
        }

        // POST: BillsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



    }
}
