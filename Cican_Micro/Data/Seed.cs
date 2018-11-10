using Cican_Micro.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cican_Micro.Data
{
    public class Seed
    {
        public static void Initialize(IApplicationBuilder app, IServiceProvider service)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (context.Produits.Count() < 2)
                {
                    context.Produits.AddRange(
                    new Produits
                    {
                        Categorie = "Électronique",
                        Modele = "74LS09",
                        ImageUrl = "74LS09.png",
                        Nom = "Puce",
                        Prix = 13.50M
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
                }
                if (context.Users.Count() < 2)
                {
                    var UserManager = service.GetRequiredService<UserManager<IdentityUser>>();


                    IdentityUser user1 = new IdentityUser
                    {
                        Email = "joe@gmail.com",
                        UserName = "joe@gmail.com",
                    };
                    IdentityUser user2 = new IdentityUser
                    {
                        Email = "bob@gmail.com",
                        UserName = "bob@gmail.com",
                    };
                    IdentityUser user3 = new IdentityUser
                    {
                        Email = "gaetan@gmail.com",
                        UserName = "gaetan@gmail.com",
                    };
                    UserManager.CreateAsync(user1, "Qwerty123!").Wait();
                    UserManager.AddToRoleAsync(user1, "Visiteur").Wait();

                    UserManager.CreateAsync(user2, "Qwerty123!").Wait();
                    UserManager.AddToRoleAsync(user2, "Administrateur").Wait();

                    UserManager.CreateAsync(user3, "Qwerty123!").Wait();
                    UserManager.AddToRoleAsync(user3, "Employe").Wait();

                }
                if (context.Commandes.Count() < 2)
                {
                    var utilisateurs = context.Users.Take(3).ToArray();
                    var produits = context.Produits.Take(3).ToArray();
                    context.Commandes.AddRange(new Commande
                    {
                        Achat = DateTime.Now,
                        UserID = utilisateurs.ElementAt(0).Id,
                        ProduitId = produits.ElementAt(0).ID
                    },
                    new Commande
                    {
                        Achat = DateTime.Now,
                        UserID = utilisateurs.ElementAt(1).Id,
                        ProduitId = produits.ElementAt(1).ID
                    },
                    new Commande
                    {
                        Achat = DateTime.Now,
                        UserID = utilisateurs.ElementAt(2).Id,
                        ProduitId = produits.ElementAt(2).ID
                    });
                }
                context.SaveChanges();
            }

        }
    }
}
