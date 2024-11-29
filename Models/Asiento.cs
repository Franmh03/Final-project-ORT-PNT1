using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CineOrt.Models
{
    public class Asiento
    {
        [Key]
        public int NumAsiento { get; set; }

        public bool EstaDisponible { get; set; }

        public int SalaId { get; set; }
        public virtual Sala Sala { get; set; }
    }
}