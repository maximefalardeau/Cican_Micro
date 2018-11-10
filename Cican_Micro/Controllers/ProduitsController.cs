using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cican_Micro.Models;
using Cican_Micro.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Cican_Micro.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public ProduitsController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Produits
        public async Task<IActionResult> Index(string searchString, string categorie)
        {
            var Produit = from m in _context.Produits
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Produit = Produit.Where(s => s.Nom.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(categorie) && categorie != "Tous")
                Produit = Produit.Where(x => x.Categorie.Contains(categorie));

            ViewData["Categories"] = _context.Produits.Select(x => x.Categorie).Distinct();
            return View(await Produit.ToListAsync());
        }
        [Authorize]
        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produits = await _context.Produits
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produits == null)
            {
                return NotFound();
            }

            return View(produits);
        }
        [Authorize(Roles ="Administrateur, Employe")]
        // GET: Produits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur, Employe")]
        public async Task<IActionResult> Create([Bind("ID,Nom,Modele,Categorie,Prix")] Produits produits, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    string rootPath = _appEnvironment.WebRootPath;
                    string imagesPath = rootPath + "\\upload_images\\" + image.FileName;
                    using (var stream = new FileStream(imagesPath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    produits.ImageUrl = image.FileName;
                }
                _context.Add(produits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produits);
        }

        // GET: Produits/Edit/5
        [Authorize(Roles = "Administrateur, Employe")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produits = await _context.Produits.FindAsync(id);
            if (produits == null)
            {
                return NotFound();
            }
            return View(produits);
        }

        // POST: Produits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur, Employe")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nom,Modele,Categorie,Prix")] Produits produits, IFormFile image)
        {
            if (id != produits.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if ( image != null && image.Length > 0)
                    {
                        string rootPath = _appEnvironment.WebRootPath;
                        string imagesPath = rootPath + "\\upload_images\\" + image.FileName;
                        using (var stream = new FileStream(imagesPath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        produits.ImageUrl = image.FileName;
                    }
                    else
                    {
                        Produits ancienProduit = _context.Produits.FirstOrDefault(x => x.ID == produits.ID);
                        if(ancienProduit != null)
                        {
                            produits.ImageUrl = ancienProduit.ImageUrl;
                        }
                        _context.Entry(ancienProduit).State = EntityState.Detached;
                    }
                    _context.Update(produits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitsExists(produits.ID))
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
            return View(produits);
        }

        // GET: Produits/Delete/5
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produits = await _context.Produits
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produits == null)
            {
                return NotFound();
            }

            return View(produits);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produits = await _context.Produits.FindAsync(id);
            _context.Produits.Remove(produits);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool ProduitsExists(int id)
        {
            return _context.Produits.Any(e => e.ID == id);
        }
    }
}
