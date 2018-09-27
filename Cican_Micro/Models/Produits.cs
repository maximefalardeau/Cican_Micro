﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cican_Micro.Models
{
    public class Produits
    {
        public int ID { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Modèle")]
        public string Modele { get; set; }
        [Display(Name = "Catégorie")]
        public string Categorie { get; set; }
        [DataType(DataType.Currency)]
        public decimal Prix { get; set; }

    }
}
