using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace CineOrt.Models
{
    public class PeliculaContext: DbContext
    {
        public DbSet<Pelicula> Peliculas { get; set; }
        public PeliculaContext()
            :base("DefaultConnection"){}

    }
}