using System.Linq;
using System.Web.Mvc;
using CineOrt.Models;
using CineOrt.Services;

namespace CineOrt.Controllers
{
    public class SalasController : Controller
    {
        private PeliculasRepository _repo;
        public SalasController()
        {
            _repo = new PeliculasRepository();
        }

        public ActionResult Details(int peliculaId, int numSala)
        {
            var sala = _repo.ObtenerSalaPorPeliculaId(peliculaId, numSala);

            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LlenarSala(int numSala, int peliculaId)
        {
            _repo.LlenarSala(numSala, peliculaId);
            return RedirectToAction("Details", new { peliculaId = peliculaId, numSala = numSala });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TomarAsiento(int numSala, int numAsiento, int peliculaId, string nombre, string apellido, string dni)
        {
            var ticket = _repo.TomarAsiento(numSala, numAsiento, peliculaId, nombre, apellido, dni);
            return RedirectToAction("DetallesTicket", new { id = ticket.TicketId });

        }
        public ActionResult DetallesTicket(int id)
        {
            var ticket = _repo.ObtenerTicketPorId(id);
            return View(ticket);
        }

    }
}