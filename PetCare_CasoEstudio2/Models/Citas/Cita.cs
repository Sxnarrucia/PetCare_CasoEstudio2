using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PetCare_CasoEstudio2.Models.Mascotas;

namespace PetCare_CasoEstudio2.Models.Citas
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        [ForeignKey("Mascota")]
        [Required(ErrorMessage = "Debe seleccionar una mascota")]
        public int MascotaId { get; set; }
        public virtual Mascota Mascota { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }  // 🔄 string, no int
        public virtual ApplicationUser Usuario { get; set; }  // 👈 relacionado con Identity

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de la cita")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage = "El motivo de la consulta es obligatorio")]
        [Display(Name = "Motivo Consulta")]
        [StringLength(100)]
        public string MotivoConsulta { get; set; }

        [Required(ErrorMessage = "El estado de la cita es obligatorio")]
        [Display(Name = "Estado de la cita")]
        [StringLength(50)]
        public string EstadoCita { get; set; }
    }
}