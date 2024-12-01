using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineOrt.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }

        [Required]
        [StringLength(100)]
        public string Pelicula { get; set; }

        [Required]
        public int AsientoId { get; set; }

        public string AsientoFila { get; set; }
        public string AsientoColumna { get; set; }

        [Required]
        [StringLength(100)]
        public string DniPersona { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompletoPersona { get; set; }
    }
}