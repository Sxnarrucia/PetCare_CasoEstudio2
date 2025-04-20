using System.Linq;
using System.Web.Http;
using APIPetCare.Filtros;
using APIPetCare.Models;

[JWTValidateFilter]
[RoutePrefix("api/appointments")]
public class AppointmentsController : ApiController
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: /api/appointments/client/{clientId} -> donde clientId es en realidad el MascotaId
    [HttpGet]
    [Route("client/{clientId}")]
    public IHttpActionResult GetCitasPendientes(int clientId) // clientId = MascotaId
    {
        var citas = db.Citas
                      .Include("Mascota")
                      .Where(c => c.MascotaId == clientId && c.EstadoCita.Trim().ToLower() == "Pendiente")
                      .Select(c => new
                      {
                          c.IdCita,
                          c.FechaCita,
                          c.MotivoConsulta,
                          Mascota = c.Mascota.NombreMascota
                      })
                      .ToList();

        return Ok(citas);
    }
}
