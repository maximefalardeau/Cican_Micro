using System;
using System.Collections.Generic;
using System.Text;
using Cican_Micro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cican_Micro.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Produits> Produits { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Commande> Commandes { get; set; }

    }
}
