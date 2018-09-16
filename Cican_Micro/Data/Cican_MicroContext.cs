using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cican_Micro.Models
{
    public class Cican_MicroContext : DbContext
    {
        public Cican_MicroContext (DbContextOptions<Cican_MicroContext> options)
            : base(options)
        {
        }

        public DbSet<Cican_Micro.Models.Produits> Produits { get; set; }
    }
}
