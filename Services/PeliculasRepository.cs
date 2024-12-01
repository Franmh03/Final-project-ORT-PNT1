using CineOrt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace CineOrt.Services
{
    public class PeliculasRepository
    {
        private PeliculaContext db;

        
        public List<Pelicula> ObtenerTodos()
        {
            using (var db = new PeliculaContext())
            {
                return db.Peliculas.Include(p => p.Salas.Select(s => s.ListaAsientos)).ToList();

            }

        }

        internal void Create(Pelicula model)
        {
            using (var db = new PeliculaContext())
            {
                if (model.Salas == null)
                {
                    model.Salas = new List<Sala>();
                }

                db.Peliculas.Add(model);
                db.SaveChanges(); // Guardar para obtener el ID de la película

                for (int i = 1; i <= 5; i++)
                {
                    var sala = new Sala { NumSala = i, PeliculaId = model.Id };
                    for (int fila = 0; fila < Sala.CANT_FILAS; fila++)
                    {
                        for (int columna = 0; columna < Sala.CANT_COLUMNAS; columna++)
                        {
                            sala.ListaAsientos.Add(new Asiento
                            {
                                NumAsiento = fila+columna,
                                Fila = fila,
                                Columna = columna,
                                EstaDisponible = true,
                                SalaId = sala.NumSala
                            });
                        }
                    }
                    db.Salas.Add(sala);
                }

                db.SaveChanges();
            }
        }

        public Pelicula GetPelicula(int id)
        {
            using (var db = new PeliculaContext())
            {
                return db.Peliculas.Include(p => p.Salas.Select(s => s.ListaAsientos)).FirstOrDefault(p => p.Id == id);


            }
        }

        public void Update(Pelicula pelicula)
        {
            using (var db = new PeliculaContext())
            {
                var peliculaAEditar = db.Peliculas.Find(pelicula.Id);
                ;
                if (peliculaAEditar != null)
                {
                    peliculaAEditar.Titulo = pelicula.Titulo;
                    peliculaAEditar.Descripcion = pelicula.Descripcion;
                    peliculaAEditar.Duracion = pelicula.Duracion;
                    peliculaAEditar.Imagen = pelicula.Imagen;

                    db.Entry(peliculaAEditar).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new PeliculaContext())
            {
                var pelicula = db.Peliculas.Include(p => p.Salas).FirstOrDefault(p => p.Id == id);

                if (pelicula != null)
                {
                    foreach (var sala in pelicula.Salas.ToList())
                    {
                        db.Salas.Remove(sala);
                    }
                    
                    db.Peliculas.Remove(pelicula);
                    db.SaveChanges();
                }

            }
        }
        public void EliminarTodasLasSalas()
        {
            using ( var db = new PeliculaContext())
            {
                var todasLasSalas = db.Salas.ToList();
                db.Salas.RemoveRange(todasLasSalas);
                db.SaveChanges();
            }
        }

        public Sala ObtenerSalaPorId(int numSala)
        {
            using (var db = new PeliculaContext())
            {
                return db.Salas.Include(s => s.ListaAsientos).FirstOrDefault(s => s.NumSala == numSala);

            }
        }
        public Sala ObtenerSalaPorPeliculaId(int peliculaId, int numSala)
        {
            using (var db = new PeliculaContext())
            {
                return db.Salas
                .Include(s => s.ListaAsientos)
                .FirstOrDefault(s => s.PeliculaId == peliculaId && s.NumSala == numSala);

            }
           
        }

        public void ActualizarPeliculasExistentes()
        {
            using (var db = new PeliculaContext())
            {
                var peliculas = db.Peliculas.Include(p => p.Salas.Select(s => s.ListaAsientos)).ToList();

                foreach (var pelicula in peliculas)
                {
                    if (pelicula.Salas.Count < 5)
                    {
                        int salasFaltantes = 5 - pelicula.Salas.Count;
                        for (int i = 1; i <= salasFaltantes; i++)
                        {
                            var sala = new Sala { NumSala = pelicula.Salas.Count + 1, PeliculaId = pelicula.Id };
                            for (int j = 1; j <= Sala.TOTAL_ASIENTOS; j++)
                            {
                                sala.ListaAsientos.Add(new Asiento { NumAsiento = j, EstaDisponible = true, SalaId = sala.NumSala });
                            }
                            db.Salas.Add(sala);
                        }
                        db.Entry(pelicula).State = EntityState.Modified;
                    }

                    foreach (var sala in pelicula.Salas)
                    {
                        if (sala.ListaAsientos.Count != Sala.TOTAL_ASIENTOS)
                        {
                            sala.ListaAsientos.Clear();
                            for (int i = 1; i <= Sala.TOTAL_ASIENTOS; i++)
                            {
                                sala.ListaAsientos.Add(new Asiento { NumAsiento = i, EstaDisponible = true, SalaId = sala.NumSala });
                            }
                        }
                    }
                }

                db.SaveChanges();
            }
        }
        public void Reseed()
        {
            using (var db = new PeliculaContext())
            {
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[Salas]', RESEED, 0);");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[Peliculas]', RESEED, 0);");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[Asientos]', RESEED, 0);");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[Tickets]', RESEED, 0);");
            }
        }
        public void LlenarSala(int numSala, int peliculaId)
        {
            using (var db = new PeliculaContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var peli = db.Peliculas.FirstOrDefault(p => p.Id == peliculaId);
                        // SQL para actualizar los asientos a no disponibles
                        var sala = peli.Salas.FirstOrDefault(s => s.NumSala == numSala);

                        // SQL para actualizar los asientos a no disponibles
                        var sqlAsientos = @"UPDATE Asientos 
                                        SET EstaDisponible = 0 
                                        WHERE SalaId = @p0";

                        db.Database.ExecuteSqlCommand(sqlAsientos, sala.NumSala);

                        // SQL para actualizar el estado de la sala a llena
                        var sqlSala = "UPDATE Salas " +
                                      "SET EstaLlena = 1 " +
                                      "WHERE NumSala = @p0 AND PeliculaId = @p1";

                        db.Database.ExecuteSqlCommand(sqlSala, new object[] { numSala, peliculaId });

                        // Confirmar la transacción
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Revertir la transacción en caso de error
                        transaction.Rollback();
                        throw;
                    }
                }
                //var pelicula = db.Peliculas
                //   .Include(p => p.Salas.Select(s => s.ListaAsientos))
                //   .FirstOrDefault(p => p.Id == peliculaId);
                //
                //var sala = pelicula?.Salas.FirstOrDefault(s => s.NumSala == numSala);
                //if (sala != null)
                //{
                //    foreach (var asiento in sala.ListaAsientos)
                //    {
                //        asiento.EstaDisponible = false;
                //        db.Entry(asiento).State = EntityState.Modified;
                //    }
                //    db.SaveChanges();
                //}
            }
        }

        public Ticket TomarAsiento(int numSala, int numAsiento, int peliculaId, string nombre, string apellido, string dni)
        {
            using (var db = new PeliculaContext())
            {
                Console.WriteLine("numSala: " + numSala);
                Console.WriteLine("numAsiento: " + numAsiento);
                Console.WriteLine("peliculaId: " + peliculaId);
                Console.WriteLine("nombre: " + nombre);
                Console.WriteLine("apellido: " + apellido);
                Console.WriteLine("dni: " + dni);
                var peli = db.Peliculas.FirstOrDefault(p => p.Id == peliculaId);
                // SQL para actualizar los asientos a no disponibles
                var sala = peli.Salas.FirstOrDefault(s => s.NumSala == numSala);

                var asiento = sala.ListaAsientos.FirstOrDefault(a => a.Id == numAsiento);
                asiento.EstaDisponible = false;
                var ticket = new Ticket
                {
                    //IdTicket = peli.Id+asiento.Id+ asiento.Fila+asiento.Columna,
                    Pelicula = peli.Titulo,
                    AsientoId = asiento.Id,
                    AsientoFila = asiento.Fila.ToString(),
                    AsientoColumna = asiento.Columna.ToString(),
                    DniPersona = dni,
                    NombreCompletoPersona = nombre + " " + apellido
                };
                        db.Tickets.Add(ticket);
                        db.SaveChanges();
                        
                    
                
                return ticket;
            }
        }
        public Ticket ObtenerTicketPorId(int id)
        {
            using (var db = new PeliculaContext())
            {
                return db.Tickets.FirstOrDefault(t => t.TicketId == id);
            }
        }

        public Ticket GenerarTicket(string peli, Asiento asiento, string dni, string nombre, string apellido)
        {
            string nombreCompleto = nombre + " " + apellido;
            return new Ticket{
                Pelicula = peli,
                AsientoId = asiento.Id,
                AsientoFila = asiento.Fila.ToString(),
                AsientoColumna = asiento.Columna.ToString(),
                DniPersona = dni,
                NombreCompletoPersona = nombreCompleto
            };
        }

    }
}