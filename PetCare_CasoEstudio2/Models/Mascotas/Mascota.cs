using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCare_CasoEstudio2.Models.Mascotas
{
    public class Mascota
    {
        [Key]
        public int IdMascota { get; set; }

        [Required(ErrorMessage = "El nombre de la mascota es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string NombreMascota { get; set; }

        [Required(ErrorMessage = "La especie es obligatoria")]
        [StringLength(50)]
        [Display(Name = "Especie")]
        public string EspecieMascota { get; set; } // perro, gato, ave, reptil, otro

        [Required(ErrorMessage = "La raza es obligatoria")]
        [StringLength(100)]
        [Display(Name = "Raza")]
        public string RazaMascota { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria")]
        [Display(Name = "Edad (en años)")]
        public int EdadMascota { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Display(Name = "Peso (en kg)")]
        public double PesoMascota { get; set; }

        // Relación con el propietario
        [Required]
        [ForeignKey("Usuario")]
        [Display(Name = "Propietario")]
        public string UsuarioId { get; set; }

        public virtual ApplicationUser Usuario { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        [Display(Name = "Usuario que Registró")]
        public string UsuarioDeRegistro { get; set; }
    }
}
