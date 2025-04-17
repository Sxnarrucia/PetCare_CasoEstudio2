using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetCare_CasoEstudio2.Models.Usuario
{
    public class Usuario
    {
        public string Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; }
    }
}