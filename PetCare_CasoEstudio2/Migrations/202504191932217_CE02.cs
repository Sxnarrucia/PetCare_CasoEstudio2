namespace PetCare_CasoEstudio2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CE02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        IdCita = c.Int(nullable: false, identity: true),
                        MascotaId = c.Int(nullable: false),
                        UsuarioId = c.String(nullable: false, maxLength: 128),
                        FechaCita = c.DateTime(nullable: false),
                        MotivoConsulta = c.String(nullable: false, maxLength: 200),
                        EstadoCita = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdCita)
                .ForeignKey("dbo.Mascotas", t => t.MascotaId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.MascotaId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Mascotas",
                c => new
                    {
                        IdMascota = c.Int(nullable: false, identity: true),
                        NombreMascota = c.String(nullable: false, maxLength: 100),
                        EspecieMascota = c.String(nullable: false, maxLength: 50),
                        RazaMascota = c.String(nullable: false, maxLength: 100),
                        EdadMascota = c.Int(nullable: false),
                        PesoMascota = c.Double(nullable: false),
                        UsuarioId = c.String(nullable: false, maxLength: 128),
                        FechaRegistro = c.DateTime(nullable: false),
                        UsuarioDeRegistro = c.String(),
                    })
                .PrimaryKey(t => t.IdMascota)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NombreCompleto = c.String(),
                        Rol = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Historials",
                c => new
                    {
                        IdHistorial = c.Int(nullable: false, identity: true),
                        CitaID = c.Int(nullable: false),
                        Diagnostico = c.String(nullable: false, maxLength: 200),
                        Tratamiento = c.String(nullable: false, maxLength: 200),
                        Observaciones = c.String(maxLength: 300),
                        FechaSeguimiento = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdHistorial)
                .ForeignKey("dbo.Citas", t => t.CitaID, cascadeDelete: true)
                .Index(t => t.CitaID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Historials", "CitaID", "dbo.Citas");
            DropForeignKey("dbo.Citas", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Citas", "MascotaId", "dbo.Mascotas");
            DropForeignKey("dbo.Mascotas", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Historials", new[] { "CitaID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Mascotas", new[] { "UsuarioId" });
            DropIndex("dbo.Citas", new[] { "UsuarioId" });
            DropIndex("dbo.Citas", new[] { "MascotaId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Historials");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Mascotas");
            DropTable("dbo.Citas");
        }
    }
}
