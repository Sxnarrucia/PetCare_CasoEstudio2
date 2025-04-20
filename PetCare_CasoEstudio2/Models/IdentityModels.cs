using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PetCare_CasoEstudio2.Models.Mascotas;
using PetCare_CasoEstudio2.Models.Citas;

namespace PetCare_CasoEstudio2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CE02", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mascota → Usuario (Propietario)
            modelBuilder.Entity<Mascota>()
                .HasRequired(m => m.Usuario)
                .WithMany()
                .HasForeignKey(m => m.UsuarioId)
                .WillCascadeOnDelete(false);

            // Cita → Usuario (Veterinario)
            modelBuilder.Entity<Cita>()
                .HasRequired(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .WillCascadeOnDelete(false);

            // Cita → Mascota
            modelBuilder.Entity<Cita>()
                .HasRequired(c => c.Mascota)
                .WithMany()
                .HasForeignKey(c => c.MascotaId)
                .WillCascadeOnDelete(false);
        }


        public System.Data.Entity.DbSet<PetCare_CasoEstudio2.Models.Historial.Historial> Historials { get; set; }
    }
}
