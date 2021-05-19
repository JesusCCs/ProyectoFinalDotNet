using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Models.Auth;

#nullable disable

namespace ProyectoFinal.DAL
{
    public class DataBaseContext : IdentityDbContext<Auth, Rol, Guid>
    {
        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=m1sql;database=proyectofinal",
                    Microsoft.EntityFrameworkCore.ServerVersion.FromString("5.7.24-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Anuncio>(entity =>
            {
                entity.ToTable("anuncios");

                entity.HasComment(
                    "La tabla que contiene los anuncios contratados por los gimnasios. Las especificaciones son las siguiente");

                entity.HasIndex(e => e.GimnasioId, "FK_anuncios_gimnasios");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.FechaActualizado)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("fechaActualizado")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FechaCreado)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreado")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("fechaInicio");

                entity.Property(e => e.GimnasioId)
                    .HasColumnName("gimnasioID")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ReproduccionesLimite)
                    .HasColumnType("int(2)")
                    .HasColumnName("reproduccionesLimite");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("enum('imagen','gif','video')")
                    .HasColumnName("tipo")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Recurso)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("recurso")
                    .HasComment("Se guarda la referencia al archivo en la carpeta anuncios");

                entity.HasOne(d => d.Gimnasio)
                    .WithMany(p => p.Anuncios)
                    .HasForeignKey(d => d.GimnasioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_anuncios_gimnasios");
            });

            modelBuilder.Entity<AnunciosUsuario>(entity =>
            {
                entity.ToTable("anuncios_usuarios");

                entity.HasIndex(e => e.AnucioId, "FK_anuncios_usuarios_anuncios");

                entity.HasIndex(e => e.UsuarioId, "FK_anuncios_usuarios_usuarios");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AnucioId)
                    .HasColumnName("anucioID")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FechaActualizado)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("fechaActualizado")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FechaCreado)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreado")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Reproducciones)
                    .HasColumnType("int(2)")
                    .HasColumnName("reproducciones");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("usuarioID")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Anucio)
                    .WithMany(p => p.AnunciosUsuarios)
                    .HasForeignKey(d => d.AnucioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_anuncios_usuarios_anuncios");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Anuncios)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_anuncios_usuarios_usuarios");
            });

            modelBuilder.Entity<Gimnasio>(entity =>
            {
                entity.ToTable("gimnasios");

                entity.HasIndex(e => e.Cif, "gimnasios_cif_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.AuthId, "FK_auth_gimnasios");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Cif)
                    .IsRequired()
                    .HasColumnType("varchar(15)")
                    .HasColumnName("cif")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("descripcion")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("direccion")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FechaActualizado)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("fechaActualizado")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FechaCreado)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreado")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Tarifa)
                    .HasColumnType("int(11)")
                    .HasColumnName("tarifa")
                    .HasComment("Es en céntimos");

                entity.Property(e => e.Logo)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("logo")
                    .HasComment(
                        "Campo útil para tener una referencia al nombre con el que se guardó el fichero subido por el usuario en wwwroot");

                entity.HasOne(d => d.Auth);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Apellidos)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("apellidos")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("direccion")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FechaActualizado)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("fechaActualizado")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FechaCreado)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreado")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Auth);
            });

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<Auth>().ToTable("auth")
                .HasData(new Auth
                {
                    Id = new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                    EmailConfirmed = true,
                    UserName = "admin",
                    NormalizedUserName = "admin".Normalize(),
                    PasswordHash = hasher.HashPassword(null, "admin"),
                    Email = "admin@email.es",
                    NormalizedEmail = "admin@email.es".Normalize()
                });


            modelBuilder.Entity<Rol>().ToTable("roles")
                .HasData(
                    new Rol
                    {
                        Id = new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                        Name = Rol.Admin,
                        NormalizedName = Rol.Admin.ToUpper()
                    },
                    new Rol
                    {
                        Id = Guid.NewGuid(),
                        Name = Rol.Gimnasio,
                        NormalizedName = Rol.Gimnasio.ToUpper()
                    }, new Rol
                    {
                        Id = Guid.NewGuid(),
                        Name = Rol.Usuario,
                        NormalizedName = Rol.Usuario.ToUpper()
                    });

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("auth_roles")
                .Property(t => t.UserId).HasColumnName("AuthId");

            modelBuilder.Entity<IdentityUserRole<Guid>>()
                .HasData(new IdentityUserRole<Guid>
                {   // Agregamos a mano los ID del usuario y el rol 'Admin'
                    RoleId = new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                    UserId = new Guid("76148582-7877-4b91-be8e-22e34376045a")
                });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("auth_claims")
                .Property(t => t.UserId).HasColumnName("AuthId");

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("auth_login")
                .Property(t => t.UserId).HasColumnName("AuthId");

            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("auth_tokens")
                .Property(t => t.UserId).HasColumnName("AuthId");
        }
    }
}