using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cican_Micro.Models
{
    public class Produits
    {
        public int ID { get; set; }
        [Display(Name = "Image"), RegularExpression(@"^.*\.(jpg|JPG|gif|png|PNG|jpeg|JPEG)$")]
        public string ImageUrl { get; set; }
        [MinLength(3, ErrorMessage = "Le nom doit contenir au minimum trois lettres"), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Doit commencer par une majuscule et ne pas contenir de chiffres")]
        public string Nom { get; set; }
        [Display(Name = "Modèle"), RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$", ErrorMessage = "Doit commencer par une majuscule")]
        public string Modele { get; set; }
        [Display(Name = "Catégorie"), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Doit commencer par une majuscule et ne pas contenir de chiffres")]
        public string Categorie { get; set; }
        [DataType(DataType.Currency)]
        public decimal Prix { get; set; }

    }
}
