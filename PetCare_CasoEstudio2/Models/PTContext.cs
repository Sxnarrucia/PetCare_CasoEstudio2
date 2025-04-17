using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PetCare_CasoEstudio2.Models.Mascotas;
using PetCare_CasoEstudio2.Models.Citas;
using PetCare_CasoEstudio2.Models.Historial;

namespace PetCare_CasoEstudio2.Models
{
    public class PTContext : DbContext
    {

        public PTContext() : base("name=PT05")
        {
        }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Cita> Citas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mascota>()
                .HasRequired(m => m.Usuario)
                .WithMany()
                .HasForeignKey(m => m.UsuarioId);
            modelBuilder.Entity<Cita>()
                .HasRequired(c => c.Mascota)
                .WithMany()
                .HasForeignKey(c => c.MascotaId);
            modelBuilder.Entity<Cita>()
                .HasRequired(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId);
            base.OnModelCreating(modelBuilder);
        }
    }
}