using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPetCare.Models.Entidades
{
    [Table("AspNetUsers")]
    public class Usuario
    {
        [Key]
        public string Id { get; set; }

        public string NombreCompleto { get; set; }
    }
}
