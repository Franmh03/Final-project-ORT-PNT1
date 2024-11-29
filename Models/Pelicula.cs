using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace CineOrt.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int Duracion { get; set; }

        public Pelicula (int idPelicula ,string titulo,string descripcion,int duracion)
        {
            this.Id = idPelicula;
            this.Titulo = titulo;
            this.Descripcion = descripcion;
            this.Duracion  = duracion;
            
        }

        public Pelicula ()
        {

        }
    }
}