using CineOrt.Models;
using CineOrt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineOrt.Controllers
{
    public class HomeController : Controller
    {

        private PeliculasRepository _repo;
        public HomeController()
        {
            _repo = new PeliculasRepository();
        }
        public ActionResult Index()
        {
            var peliculas = _repo.ObtenerTodos();
            return View(peliculas);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
               
        public ActionResult Pelicula (int iDpelicula)
        {
            
            return View(iDpelicula);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}