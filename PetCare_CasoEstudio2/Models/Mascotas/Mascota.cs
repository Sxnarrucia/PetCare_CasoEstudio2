using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetCare_CasoEstudio2.Models.Mascotas
{
    public class Mascota
    {
        [Key]
        public int IdMascota { get; set; }

        [Required(ErrorMessage = "El nombre de la mascota es obligatorio")]
        [StringLength(100)]
        public string NombreMascota { get; set; }

        [Required(ErrorMessage = "La especie de la mascota es obligatorio")]
        [StringLength(100)]
        public string EspecieMascota { get; set; }

        [Required(ErrorMessage = "La raza de la mascota es obligatorio")]
        [StringLength(100)]
        public string RazaMascota { get; set; }

        [Required(ErrorMessage = "La edad de la mascota es obligatorio")]
        public int EdadMascota { get; set; }

        [Required(ErrorMessage = "El peso de la mascota es obligatorio")]
        public double PesoMascota { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }  // 🔄 string, no int

        public virtual ApplicationUser Usuario { get; set; }  // 👈 relacionado con Identity

        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; }

        public string UsuarioDeRegistro { get; set; }

    }
}