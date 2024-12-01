using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CineOrt.Models
{
    [Table("Asientos")]
    public class Asiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int NumAsiento { get; set; }

        public bool EstaDisponible { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }

        [ForeignKey("Sala")]
        public int SalaId { get; set; }
        public virtual Sala Sala { get; set; }
    }
}