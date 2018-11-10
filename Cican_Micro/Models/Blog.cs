using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cican_Micro.Models
{
    public class Blog
    {
        [Required]
        public int id { get; set; }
        [Required, StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Author { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

    }
}
