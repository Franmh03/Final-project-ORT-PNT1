using CineOrt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CineOrt.Services
{
    public class PeliculasRepository
    {
        public List<Pelicula> ObtenerTodos() {
            using (var db = new PeliculaContext())
            {
                return db.Peliculas.ToList();
            }
                
        }

        internal void Create(Pelicula model)
        {
            using (var db = new PeliculaContext())
            {
                db.Peliculas.Add(model);
                db.SaveChanges();
            }
        }
    }
}