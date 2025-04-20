using System.Collections.Generic;
using System.Data.Entity;
using APIPetCare.Models.Entidades;

namespace APIPetCare.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Asegúrate que el nombre coincida con tu connectionString en Web.config
        public ApplicationDbContext() : base("DbContextLocal") { }

        public DbSet<Cita> Citas { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Historial> Historials { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones explícitas si se necesitan
            modelBuilder.Entity<Cita>()
                        .HasRequired(c => c.Mascota)
                        .WithMany()
                        .HasForeignKey(c => c.MascotaId);

            modelBuilder.Entity<Historial>()
                        .HasRequired(h => h.Cita)
                        .WithMany()
                        .HasForeignKey(h => h.CitaID);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
