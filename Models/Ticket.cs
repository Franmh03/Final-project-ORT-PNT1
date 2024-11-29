using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CineOrt.Models
{
    public class Ticket
    {
        [Key]
        public string IdTicket { get; set; }

        [Required]
        [StringLength(100)]
        public string Pelicula { get; set; }

        [Required]
        public double Horario { get; set; }
                
        [Required]
        [StringLength(100)]
        public string NombreCompletoPersona { get; set; }
    }
}