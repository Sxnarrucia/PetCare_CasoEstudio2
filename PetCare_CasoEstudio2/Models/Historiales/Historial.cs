using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetCare_CasoEstudio2.Models.Citas;

namespace PetCare_CasoEstudio2.Models.Historial
{
    public class Historial
    {
        [Key]
        public int IdHistorial { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una cita")]
        [ForeignKey("Cita")]
        public int CitaID { get; set; }
        public virtual Cita Cita { get; set; }

        [Required(ErrorMessage = "El diagnóstico es obligatorio")]
        [StringLength(200)]
        public string Diagnostico { get; set; }

        [Required(ErrorMessage = "El tratamiento es obligatorio")]
        [StringLength(200)]
        public string Tratamiento { get; set; }

        [StringLength(300)]
        [Display(Name = "Observaciones adicionales")]
        public string Observaciones { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de seguimiento (opcional)")]
        public DateTime? FechaSeguimiento { get; set; } 
    }
}
