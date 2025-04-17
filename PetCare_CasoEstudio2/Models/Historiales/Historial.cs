using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PetCare_CasoEstudio2.Models.Citas;

namespace PetCare_CasoEstudio2.Models.Historial
{
    public class Historial
    {
        [Key]
        public int IdHistorial { get; set; }

        [ForeignKey("Cita")]
        [Required(ErrorMessage = "Debe seleccionar una cita")]
        public int CitaID { get; set; }
        public virtual Cita Cita { get; set; }

        [Required(ErrorMessage = "El diagnostico es obligatorio")]
        [Display(Name = "Diagnostico")]
        [StringLength(100)]
        public string Diagnostico { get; set; }

        [Required(ErrorMessage = "El tratamiento es obligatorio")]
        [Display(Name = "Tratamiento")]
        [StringLength(100)]
        public string Tratamiento { get; set; }

        [Display(Name = "Observaciones Adicionales")]
        [StringLength(100)]
        public string Observaciones { get; set; }
    }
}