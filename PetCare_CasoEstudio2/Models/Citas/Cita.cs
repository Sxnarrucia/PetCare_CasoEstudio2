using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetCare_CasoEstudio2.Models.Mascotas;

namespace PetCare_CasoEstudio2.Models.Citas
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        // Mascota relacionada
        [Required(ErrorMessage = "Debe seleccionar una mascota")]
        [ForeignKey("Mascota")]
        public int MascotaId { get; set; }
        public virtual Mascota Mascota { get; set; }

        // Veterinario asignado (usuario)
        [Required(ErrorMessage = "Debe asignar un veterinario")]
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        [Required(ErrorMessage = "La fecha y hora de la cita son obligatorias")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha y Hora")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage = "El motivo de la consulta es obligatorio")]
        [Display(Name = "Motivo de la consulta")]
        [StringLength(200)]
        public string MotivoConsulta { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un estado")]
        [Display(Name = "Estado")]
        [StringLength(50)]
        public string EstadoCita { get; set; } // Pendiente, Realizada, Cancelada

        public Cita()
        {
            EstadoCita = "Pendiente";
        }

        [NotMapped]
        public string DescripcionExtendida
        {
            get
            {
                return $"{Mascota?.NombreMascota} con {Usuario?.NombreCompleto} el {FechaCita.ToString("dd/MM/yyyy HH:mm")}";
            }
        }

    }
}
