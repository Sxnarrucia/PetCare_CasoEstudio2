using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PetCare_CasoEstudio2.Models;
using PetCare_CasoEstudio2.Models.Mascotas;

namespace PetCare_CasoEstudio2.Controllers
{
    [Authorize]
    public class MascotasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mascotas
        public ActionResult Index()
        {
            var mascotas = db.Mascotas.Include(m => m.Usuario);
            return View(mascotas.ToList());
        }

        // GET: Mascotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Mascota mascota = db.Mascotas.Include(m => m.Usuario).FirstOrDefault(m => m.IdMascota == id);
            if (mascota == null) return HttpNotFound();

            return View(mascota);
        }

        // GET: Mascotas/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "NombreCompleto");
            ViewBag.Especies = new SelectList(new[] { "Perro", "Gato", "Ave", "Reptil", "Otro" });
            return View();
        }

        // POST: Mascotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMascota,NombreMascota,EspecieMascota,RazaMascota,EdadMascota,PesoMascota,UsuarioId")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                mascota.FechaRegistro = DateTime.Now;
                mascota.UsuarioDeRegistro = User.Identity.Name;
                db.Mascotas.Add(mascota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "NombreCompleto", mascota.UsuarioId);
            ViewBag.Especies = new SelectList(new[] { "Perro", "Gato", "Ave", "Reptil", "Otro" }, mascota.EspecieMascota);
            return View(mascota);
        }


        // GET: Mascotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Mascota mascota = db.Mascotas.Find(id);
            if (mascota == null) return HttpNotFound();

            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "NombreCompleto", mascota.UsuarioId);
            ViewBag.Especies = new SelectList(new[] { "Perro", "Gato", "Ave", "Reptil", "Otro" }, mascota.EspecieMascota);
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMascota,NombreMascota,EspecieMascota,RazaMascota,EdadMascota,PesoMascota,UsuarioId")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                var original = db.Mascotas.AsNoTracking().FirstOrDefault(m => m.IdMascota == mascota.IdMascota);
                if (original == null) return HttpNotFound();

                mascota.FechaRegistro = original.FechaRegistro;
                mascota.UsuarioDeRegistro = original.UsuarioDeRegistro;

                db.Entry(mascota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "NombreCompleto", mascota.UsuarioId);
            ViewBag.Especies = new SelectList(new[] { "Perro", "Gato", "Ave", "Reptil", "Otro" }, mascota.EspecieMascota);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Mascota mascota = db.Mascotas.Include(m => m.Usuario).FirstOrDefault(m => m.IdMascota == id);
            if (mascota == null) return HttpNotFound();

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mascota mascota = db.Mascotas.Find(id);
            db.Mascotas.Remove(mascota);
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
