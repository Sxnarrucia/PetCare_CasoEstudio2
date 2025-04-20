using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIPetCare.Models.Entidades
{
    public class Mascota
    {
        [Key]
        public int IdMascota { get; set; }

        public string NombreMascota { get; set; }

        public string EspecieMascota { get; set; }

        public string RazaMascota { get; set; }

        public int EdadMascota { get; set; }

        public float PesoMascota { get; set; }

        // Relación con Citas
        public virtual ICollection<Cita> Citas { get; set; }
    }
}
