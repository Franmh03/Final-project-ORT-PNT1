using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace CineOrt.Models
{
    public class Sala
    {
        public const int CANT_FILAS = 8;
        public const int CANT_COLUMNAS = 25;

        [Key]
        public int NumSala { get; set; }

        public bool EstaLlena { get; set; }

        public virtual ICollection<Asiento> ListaAsientos { get; set; }

        public int PeliculaId { get; set; }
        public virtual Pelicula Pelicula { get; set; }
    }
}