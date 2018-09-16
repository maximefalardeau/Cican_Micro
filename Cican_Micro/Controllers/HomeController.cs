using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cican_Micro.Models;

namespace Cican_Micro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "À propos de nous";

            return View();
        }
        public IActionResult Produits()
        {
            ViewData["Message"] = "Nos produits";

            return View();
        }
        public IActionResult NProduits()
        {
            ViewData["Message"] = "Nos nouveaux produits";

            return View();
        }
        public IActionResult Liquidations()
        {
            ViewData["Message"] = "Nos liquidations";

            return View();
        }
        public IActionResult Expedition()
        {
            ViewData["Message"] = "Informations à propos de l'expédition";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Contactez-nous pour toutes vos questions au sujet de nos produits";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
