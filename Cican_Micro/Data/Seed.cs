using Cican_Micro.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cican_Micro.Data
{
    public class Seed
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (context.Produits.Count() > 2)
                {
                    return;
                }
                context.Produits.AddRange(
                    new Produits
                    {
                        Categorie = "Électronique",
                        Modele = "74LS09",
                        ImageUrl = "74LS09.png",
                        Nom ="Puce",
                        Prix= 13.50M
                    },
                    new Produits
                    {
                        Categorie = "Électronique",
                        Modele = "74LS32",
                        ImageUrl = "74LS32.png",
                        Nom = "Micro-processeur",
                        Prix = 15.50M
                    },
                    new Produits
                    {
                        Categorie = "Électronique",
                        Modele = "74LS32",
                        ImageUrl = "74LS32.png",
                        Nom = "Thermomètre",
                        Prix = 18.50M
                    }
                    );
                context.SaveChanges();
            }

        }
    }
}
