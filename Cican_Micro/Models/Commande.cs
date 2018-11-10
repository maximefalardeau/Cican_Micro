using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cican_Micro.Models
{
    public class Commande
    {
        public int CommandeID { get; set; }
        public string UserID { get; set; }
        public int ProduitId { get; set; }
        public DateTime Achat { get; set; }
        public IdentityUser User { get; set; }
        public Produits Produit { get; set; }
    }
}
