using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cican_Micro.Data;
using Cican_Micro.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cican_Micro.Controllers
{
    [Authorize(Roles ="Employe,Administrateur")]
    public class CommandesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommandesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Commandes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Commandes.Include(c => c.Produit).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Commandes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes
                .Include(c => c.Produit)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CommandeID == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // GET: Commandes/Create
        public IActionResult Create()
        {
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ID", "Nom");
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Commandes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommandeID,UserID,ProduitId")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                commande.Achat = DateTime.Now;
                _context.Add(commande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ID", "Nom", commande.ProduitId);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "UserName", commande.UserID);
            return View(commande);
        }

        

        // GET: Commandes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes
                .Include(c => c.Produit)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CommandeID == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // POST: Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);
            _context.Commandes.Remove(commande);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeExists(int id)
        {
            return _context.Commandes.Any(e => e.CommandeID == id);
        }
    }
}
