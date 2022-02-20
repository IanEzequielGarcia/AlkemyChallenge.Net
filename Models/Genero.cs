using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIDisney2.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        public string ImagenGenero { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int PeliculasAsociadas { get; set; }
    }
}
