using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public partial class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anuncio> Anuncios { get; set; }
        public virtual DbSet<AnunciosUsuario> AnunciosUsuarios { get; set; }
        public virtual DbSet<Auth> Auths { get; set; }
        public virtual DbSet<Gimnasio> Gimnasios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=m1sql;database=proyectofinal", Microsoft.EntityFrameworkCore.ServerVersion.FromString("5.7.24-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anuncio>(entity =>
            {
                entity.ToTable("anuncios");

                entity.HasComment("La tabla que contiene los anuncios contratados por los gimnasios. Las especificaciones son las siguiente");

                entity.HasIndex(e => e.GimnasioId, "FK_anuncios_gimnasios");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

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
                    .HasColumnType("int(11)")
                    .HasColumnName("gimnasioID");

                entity.Property(e => e.ReproduccionesLimite)
                    .HasColumnType("int(2)")
                    .HasColumnName("reproduccionesLimite");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("enum('imagen','gif','video')")
                    .HasColumnName("tipo")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<AnunciosUsuario>(entity =>
            {
                entity.ToTable("anuncios_usuarios");

                entity.HasIndex(e => e.AnucioId, "FK_anuncios_usuarios_anuncios");

                entity.HasIndex(e => e.UsuarioId, "FK_anuncios_usuarios_usuarios");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AnucioId)
                    .HasColumnType("int(11)")
                    .HasColumnName("anucioID");

                entity.Property(e => e.Reproducciones)
                    .HasColumnType("int(2)")
                    .HasColumnName("reproducciones");

                entity.Property(e => e.UsuarioId)
                    .HasColumnType("int(11)")
                    .HasColumnName("usuarioID");
            });

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.ToTable("auth");

                entity.HasComment("Tabla que envuelve a la autenticación. Los usuarios y los gimnasios heredarán de ésta, ya que ambas entidades necesitan de loguearse en la aplicación móvil y en la web, respectivamente.");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.HasIndex(e => e.Login, "login")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("email")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasColumnName("login")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("password")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("enum('gimnasio','usuario')")
                    .HasColumnName("tipo")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Gimnasio>(entity =>
            {
                entity.ToTable("gimnasios");

                entity.HasIndex(e => e.AuthId, "FK_gimnasios_auth");

                entity.HasIndex(e => e.Cif, "gimnasios_cif_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AuthId)
                    .HasColumnType("int(11)")
                    .HasColumnName("authID");

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
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.AuthId, "FK_usuarios_auth");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Apellidos)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("apellidos")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AuthId)
                    .HasColumnType("int(11)")
                    .HasColumnName("authID");

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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
