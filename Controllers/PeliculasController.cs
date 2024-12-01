using CineOrt.Models;
using CineOrt.Services;
using System;
using System.Web.Mvc;

namespace CineOrt.Controllers
{
    public class PeliculasController : Controller
    {
        private PeliculasRepository _repo;
        public PeliculasController()
        {
            _repo = new PeliculasRepository();
        }

        // GET: Peliculas
        public ActionResult Index()
        {
            var model = _repo.ObtenerTodos();
            return View(model);
        }
        //En el index vamos a retornar un listado con todas las peliculas que tenemos (La cartelera)

        // GET: Peliculas/Details/5
        public ActionResult Details(int id)
        {
            var pelicula = _repo.GetPelicula(id); 
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // GET: Peliculas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Peliculas/Create
        [HttpPost]
        public ActionResult Create(Pelicula model)
        {
            if (ModelState.IsValid)
            {
                _repo.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Peliculas/Edit/5
        public ActionResult Edit(int id)
        {
            var pelicula = _repo.GetPelicula(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descripcion,Duracion,Imagen")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(pelicula);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the error (uncomment the line below once you have a logging mechanism)
                    // Log.Error(ex);
                    ModelState.AddModelError("", "No se pudo guardar los cambios. Inténtalo de nuevo, y si el problema persiste, consulta con el administrador del sistema.");
                }
            }
            return View(pelicula);
        }



        // GET: Peliculas/Delete/5
        public ActionResult Delete(int id)
        {
            var pelicula = _repo.GetPelicula(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repo.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return RedirectToAction("Index", new { error = true });
            }
        }
        // Nueva acción para actualizar las películas existentes
        public ActionResult ActualizarPeliculasExistentes()
        {
            _repo.ActualizarPeliculasExistentes();
            return RedirectToAction("Index");
        }
        public ActionResult EliminarTodasLasSalas()
        {
            _repo.EliminarTodasLasSalas();
            return RedirectToAction("Index"); // Redirige a la lista de películas después de eliminar todas las salas
        }
    }
}
