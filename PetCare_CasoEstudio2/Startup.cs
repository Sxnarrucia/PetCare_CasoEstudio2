using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using PetCare_CasoEstudio2.Models;

[assembly: OwinStartupAttribute(typeof(PetCare_CasoEstudio2.Startup))]
namespace PetCare_CasoEstudio2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CrearRolesYUsuarios();
        }

        private void CrearRolesYUsuarios()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string[] roles = { "Administrador", "Veterinario", "Asistente", "Cliente" };

            foreach (var rol in roles)
            {
                if (!roleManager.RoleExists(rol))
                {
                    var role = new IdentityRole(rol);
                    roleManager.Create(role);
                }
            }

            // (Opcional) Crear un superadmin por defecto
            if (userManager.FindByName("admin") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@petcare.com",
                    NombreCompleto = "Administrador del Sistema",
                    Rol = "Administrador"
                };

                var result = userManager.Create(user, "Admin123!");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrador");
                }
            }
        }
    }
}
