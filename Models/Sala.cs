using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineOrt.Models
{
    [Table("Salas")]
    public class Sala
    {
        public const int CANT_FILAS = 8;
        public const int CANT_COLUMNAS = 25;
        public const int TOTAL_ASIENTOS = CANT_FILAS * CANT_COLUMNAS;

        [Key]
        public int NumSala { get; set; }

        public bool EstaLlena { get; set; }

        public virtual ICollection<Asiento> ListaAsientos { get; set; }

        [ForeignKey("Pelicula")]
        public int PeliculaId { get; set; }
        public virtual Pelicula Pelicula { get; set; }

        public Sala()
        {
            ListaAsientos = new List<Asiento>();
            //InicializarAsientos();
        }

       //private void InicializarAsientos()
       //{
       //    for (int i = 0; i < CANT_FILAS; i++)
       //    {
       //        for (int j = 0; j < CANT_COLUMNAS; j++)
       //        {
       //            ListaAsientos.Add(new Asiento { Fila = i, Columna = j, EstaDisponible = true, SalaId = NumSala });
       //        }
       //    }
       //}
    }
}
