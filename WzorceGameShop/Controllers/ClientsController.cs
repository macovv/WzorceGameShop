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

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
