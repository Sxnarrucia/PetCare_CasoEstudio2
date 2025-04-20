using System.Linq;
using System.Web.Http;
using APIPetCare.Filtros;
using APIPetCare.Models;

[JWTValidateFilter]
public class PetsController : ApiController
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: /api/pets/{petId}/medical-history
    [HttpGet]
    [Route("{petId}/medical-history")]
    public IHttpActionResult GetHistorialMedico(int petId)
    {
        if (petId <= 0)
            return BadRequest("El ID de la mascota es inválido.");

        var historial = db.Historials
                          .Where(h => h.Cita.MascotaId == petId)
                          .Select(h => new {
                              h.IdHistorial,
                              h.Diagnostico,
                              h.Tratamiento,
                              h.Observaciones,
                              Veterinario = h.Cita.Usuario.NombreCompleto,
                              Fecha = h.Cita.FechaCita
                          }).ToList();

        return Ok(historial);
    }

}
