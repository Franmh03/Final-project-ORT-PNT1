using CineOrt.Models;
using CineOrt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
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
            try
            {
                if (ModelState.IsValid) {
                    _repo.Create(model);
                    return RedirectToAction("Index");
                }

                
            }
            catch (Exception ex)
            {
                //Log
            }

            return View(model);
        }

        // GET: Peliculas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Peliculas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Peliculas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Peliculas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
