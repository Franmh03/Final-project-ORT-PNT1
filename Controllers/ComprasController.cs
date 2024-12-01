using System.Linq;
using System.Web.Mvc;
using CineOrt.Models;
using CineOrt.Services;

namespace CineOrt.Controllers
{
    public class ComprasController : Controller
    {
        private PeliculasRepository _repo;
        public ComprasController()
        {
            _repo = new PeliculasRepository();
        }

       //[HttpPost]
       //[ValidateAntiForgeryToken]
       //public ActionResult TomarAsiento(int numSala, int numAsiento, int peliculaId, string nombre, string apellido, string dni)
       //{
       //    
       //    var ticket = _repo.TomarAsiento(numSala, numAsiento, peliculaId, dni, nombre, apellido);
       //    return View("Ticket", ticket);
       //}
    }
}