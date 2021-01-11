using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WzorceGameShop.Data;
using WzorceGameShop.Models;
using WzorceGameShop.ViewModel;

namespace WzorceGameShop.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameShopContext _context;

        public GamesController(GameShopContext context)
        {
            _context = context;
        }

        // GET: Games
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            //var client = new Client() { Login = "Jano", Name = "Jan Kowalski", Saldo = 400 };
            //_context.Clients.Add(client);
            //_context.SaveChanges();

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

            var games = from x in _context.Games select x;
            

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(x =>
                        x.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    games = games.OrderByDescending(x => x.Name);
                    break;
                case "category":
                    games = games.OrderBy(x => x.CategoryId);
                    break;
                case "categoryDesc":
                    games = games.OrderByDescending(x => x.CategoryId);
                    break;
                case "studio":
                    games = games.OrderBy(x => x.StudioId);
                    break;
                case "studioDesc":
                    games = games.OrderByDescending(x => x.StudioId);
                    break;
                case "price":
                    games = games.OrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    games = games.OrderByDescending(x => x.Price);
                    break;
                case "pg":
                    games = games.OrderBy(x => x.PG);
                    break;
                case "pgDesc":
                    games = games.OrderByDescending(x => x.PG);
                    break;
                default:
                    games = games.OrderBy(x => x.Name);
                    break;
            }
            return View(games);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        // GET: Games/Create
        public async Task<IActionResult> Create()
        {
            Game game = new Game();
            List<Category> categories;
            List<Studio> studios;

            categories = await _context.Categories.ToListAsync();
            studios = await _context.Studios.ToListAsync();


            CreateGameViewModel vm = new CreateGameViewModel(game, categories, studios);

            return View(vm);
        }


        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Price,Promotion,Description,PG")] Game game)
        public async Task<IActionResult> Create(CreateGameViewModel vm)
        {
            //Category category = _context.Categories.Find(vm.IdCategory);
            //Studio studio = _context.Studios.Find(vm.IdStudio);

            var game = new Game
            {
                Name = vm.Game.Name,
                CategoryId = vm.Game.CategoryId,
                StudioId = vm.Game.StudioId,
                Price = vm.Game.Price,
                Promotion = vm.Game.Promotion,
                Description = vm.Game.Description,
                PG = vm.Game.PG,
            };
            //if (ModelState.IsValid)
            //{
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(vm);
        }


        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Promotion,Description,PG")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
