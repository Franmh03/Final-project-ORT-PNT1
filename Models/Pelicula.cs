using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineOrt.Models
{
    [Table("Peliculas")]
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

        [Required]
        public string Imagen { get; set; }

        public virtual ICollection<Sala> Salas { get; set; }

        public Pelicula(int idPelicula, string titulo, string descripcion, int duracion, string imagen)
        {
            this.Id = idPelicula;
            this.Titulo = titulo;
            this.Descripcion = descripcion;
            this.Duracion = duracion;
            this.Imagen = imagen;
            this.Salas = new List<Sala>();
        }

        public Pelicula ()
        {
            Salas = new List<Sala>();
        }
    }
}