using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetCare_CasoEstudio2.Models;
using PetCare_CasoEstudio2.Models.Citas;

namespace PetCare_CasoEstudio2.Controllers
{
    [Authorize]
    public class CitasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Citas
        public ActionResult Index()
        {
            var citas = db.Citas.Include(c => c.Mascota).Include(c => c.Usuario);
            return View(citas.ToList());
        }

        // GET: Citas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            ViewBag.MascotaId = new SelectList(db.Mascotas, "IdMascota", "NombreMascota");
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "NombreCompleto");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCita,MascotaId,UsuarioId,FechaCita,MotivoConsulta")] Cita cita)
        {
            cita.FechaCita = DateTime.SpecifyKind(cita.FechaCita, DateTimeKind.Local); // ✅

            // Validación: Fecha no puede ser en el pasado
            if (cita.FechaCita < DateTime.Now)
            {
                ModelState.AddModelError("FechaCita", "La fecha no puede estar en el pasado.");
            }

            // Validación: Solo horario laboral (8am - 6pm)
            var hora = cita.FechaCita.TimeOfDay;
            if (hora < TimeSpan.FromHours(8) || hora > TimeSpan.FromHours(18))
            {
                ModelState.AddModelError("FechaCita", "La hora debe estar entre 8:00 a.m. y 6:00 p.m.");
            }

            // Validación: Veterinario no puede tener dos citas al mismo tiempo
            bool existeConflicto = db.Citas.Any(c =>
                c.UsuarioId == cita.UsuarioId &&
                DbFunctions.TruncateTime(c.FechaCita) == DbFunctions.TruncateTime(cita.FechaCita) &&
                c.FechaCita.Hour == cita.FechaCita.Hour &&
                c.FechaCita.Minute == cita.FechaCita.Minute
            );

            if (existeConflicto)
            {
                ModelState.AddModelError("FechaCita", "Este veterinario ya tiene una cita programada en ese horario.");
            }

            if (ModelState.IsValid)
            {
                cita.EstadoCita = "Pendiente"; // ✅ Aquí se asigna si no está en el constructor
                db.Citas.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MascotaId = new SelectList(db.Mascotas, "IdMascota", "NombreMascota", cita.MascotaId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "NombreCompleto", cita.UsuarioId);
            return View(cita);
        }



        // GET: Citas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }

            ViewBag.MascotaId = new SelectList(db.Mascotas, "IdMascota", "NombreMascota", cita.MascotaId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "NombreCompleto", cita.UsuarioId);
            ViewBag.EstadoCita = new SelectList(new[] { "Pendiente", "Realizada", "Cancelada" }, cita.EstadoCita);

            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCita,MascotaId,UsuarioId,FechaCita,MotivoConsulta,EstadoCita")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MascotaId = new SelectList(db.Mascotas, "IdMascota", "NombreMascota", cita.MascotaId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "NombreCompleto", cita.UsuarioId);
            ViewBag.EstadoCita = new SelectList(new[] { "Pendiente", "Realizada", "Cancelada" }, cita.EstadoCita);


            return View(cita);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.Citas.Find(id);
            db.Citas.Remove(cita);
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
