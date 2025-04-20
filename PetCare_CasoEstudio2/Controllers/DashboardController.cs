using System;
using System.Linq;
using System.Web.Mvc;
using PetCare_CasoEstudio2.Models;

[Authorize(Roles = "Administrador")]
public class DashboardController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public JsonResult GetDashboardData()
    {
        var hoy = DateTime.Today;
        var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);

        var citasHoy = db.Citas.Count(c => c.FechaCita == hoy);
        var mascotasTotal = db.Mascotas.Count();
        var citasMes = db.Citas.Count(c => c.EstadoCita == "Realizada" && c.FechaCita >= inicioMes);

        var topVeterinarios = db.Citas
            .Where(c => c.EstadoCita == "Realizada")
            .GroupBy(c => c.Usuario.NombreCompleto)
            .Select(g => new { Veterinario = g.Key, Total = g.Count() })
            .OrderByDescending(g => g.Total)
            .Take(5)
            .ToList();

        return Json(new
        {
            citasHoy,
            mascotasTotal,
            citasMes,
            topVeterinarios
        }, JsonRequestBehavior.AllowGet);
    }
}
