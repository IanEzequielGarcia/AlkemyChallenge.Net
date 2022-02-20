using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIDisney2.Models
{
    public class Personaje
    {
        [Key]
        public int Id { get; set; }
        public string ImagenPersonaje { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
        public int PeliculasId { get; set; }
    }
}
