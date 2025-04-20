using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PetCare_CasoEstudio2.Models;
using PetCare_CasoEstudio2.Models.Historial;
using PetCare_CasoEstudio2.Models.Citas;

namespace PetCare_CasoEstudio2.Controllers
{
    [Authorize(Roles = "Administrador, Veterinario")]
    public class HistorialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Historials
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            IQueryable<Historial> historiales = db.Historials
                .Include(h => h.Cita.Mascota)
                .Include(h => h.Cita.Usuario);

            if (User.IsInRole("Administrador"))
            {
                return View(historiales.ToList());
            }
            else if (User.IsInRole("Veterinario"))
            {
                return View(historiales.Where(h => h.Cita.UsuarioId == userId).ToList());
            }
            else
            {
                return View(historiales.Where(h => h.Cita.Mascota.UsuarioId == userId).ToList());
            }
        }

        // GET: Historials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var historial = db.Historials
                .Include(h => h.Cita.Mascota)
                .Include(h => h.Cita.Usuario)
                .FirstOrDefault(h => h.IdHistorial == id);

            if (historial == null)
                return HttpNotFound();

            return View(historial);
        }

        // GET: Historials/Create
        [Authorize(Roles = "Veterinario,Administrador")]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();

            var citas = db.Citas
                .Include(c => c.Mascota)
                .Include(c => c.Usuario)
                .Where(c => c.UsuarioId == userId && c.EstadoCita == "Realizada")
                .ToList();

            ViewBag.CitaID = new SelectList(citas, "IdCita", "DescripcionExtendida");
            return View();
        }

        // POST: Historials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Veterinario,Administrador")]
        public ActionResult Create([Bind(Include = "CitaID,Diagnostico,Tratamiento,Observaciones,FechaSeguimiento")] Historial historial)
        {
            if (ModelState.IsValid)
            {
                db.Historials.Add(historial);

                var cita = db.Citas.Find(historial.CitaID);
                if (cita != null)
                {
                    cita.EstadoCita = "Finalizada";
                    db.Entry(cita).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var userId = User.Identity.GetUserId();
            var citas = db.Citas
                .Include(c => c.Mascota)
                .Include(c => c.Usuario)
                .Where(c => c.UsuarioId == userId && c.EstadoCita == "Realizada")
                .ToList();

            ViewBag.CitaID = new SelectList(citas, "IdCita", "DescripcionExtendida", historial.CitaID);
            return View(historial);
        }

        // GET: Historials/Edit/5
        [Authorize(Roles = "Veterinario,Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var historial = db.Historials.Find(id);
            if (historial == null)
                return HttpNotFound();

            ViewBag.CitaID = new SelectList(db.Citas, "IdCita", "IdCita", historial.CitaID);
            return View(historial);
        }

        // POST: Historials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Veterinario,Administrador")]
        public ActionResult Edit([Bind(Include = "IdHistorial,CitaID,Diagnostico,Tratamiento,Observaciones,FechaSeguimiento")] Historial historial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CitaID = new SelectList(db.Citas, "IdCita", "IdCita", historial.CitaID);
            return View(historial);
        }

        // GET: Historials/Delete/5
        [Authorize(Roles = "Veterinario,Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var historial = db.Historials
                .Include(h => h.Cita.Mascota)
                .Include(h => h.Cita.Usuario)
                .FirstOrDefault(h => h.IdHistorial == id);

            if (historial == null)
                return HttpNotFound();

            return View(historial);
        }

        // POST: Historials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Veterinario,Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            var historial = db.Historials.Find(id);
            db.Historials.Remove(historial);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}