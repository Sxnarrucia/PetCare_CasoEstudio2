using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPetCare.Models.Entidades
{
    [Table("Citas")]
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        public DateTime FechaCita { get; set; }

        public string MotivoConsulta { get; set; }

        public string EstadoCita { get; set; }

        // FK hacia Mascota
        public int MascotaId { get; set; }

        [ForeignKey("MascotaId")]
        public virtual Mascota Mascota { get; set; }

        // FK hacia Usuario
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        // Relación con Historial
        public virtual ICollection<Historial> Historiales { get; set; }
    }
}
