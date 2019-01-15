using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Imagen
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Imagen")]
        public string ImagePath { get; set; }
    }
}