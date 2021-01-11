using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WzorceGameShop.Data;
using WzorceGameShop.Models;

namespace WzorceGameShop.Controllers
{
    public class ClientsController : Controller
    {
        private readonly GameShopContext _context;

        public ClientsController(GameShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Login,Saldo")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Games");
            }
            return View(client);
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Id == 1); // chyba na sztywno najlepiej poki co

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["CategorySortParam"] = sortOrder == "category" ? "categoryDesc" : "category";
            ViewData["StudioSortParam"] = sortOrder == "studio" ? "studioDesc" : "studio";
            ViewData["PriceSortParam"] = sortOrder == "price" ? "priceDesc" : "price";
            ViewData["PGSortParam"] = sortOrder == "pg" ? "pgDesc" : "pg";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var purchasedGames = from x in _context.ClientsGames select x;
            var games = from x in _context.Games select x;
            var clientsGame = new List<Game>();
            foreach (var game in games)
            {
                foreach (var purchuasedGame in purchasedGames)
                {
                    if(purchuasedGame.GameId == game.Id)
                    {
                        clientsGame.Add(game);
                    }
                }
            }
            return View(clientsGame);
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
