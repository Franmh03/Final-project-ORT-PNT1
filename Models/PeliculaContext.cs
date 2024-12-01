using System.Data.Entity;


namespace CineOrt.Models
{
    public class PeliculaContext: DbContext
    {
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Asiento> Asientos { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public PeliculaContext()
            :base("DefaultConnection"){}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}