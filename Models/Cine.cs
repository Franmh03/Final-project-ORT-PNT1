using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace CineOrt.Models
{
    public class Cine
    {
        public const int CANT_SALAS = 20;

        [Key]
        public int Id { get; set; }

        public virtual ICollection<Sala> ListaSalas { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}