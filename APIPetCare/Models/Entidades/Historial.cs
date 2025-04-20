using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPetCare.Models.Entidades
{
    public class Historial
    {
        [Key]
        public int IdHistorial { get; set; }

        public string Diagnostico { get; set; }

        public string Tratamiento { get; set; }

        public string Observaciones { get; set; }

        public DateTime? FechaSeguimiento { get; set; }

        // FK hacia Cita
        public int CitaID { get; set; }

        [ForeignKey("CitaID")]
        public virtual Cita Cita { get; set; }
    }
}
