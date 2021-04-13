using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        public virtual DbSet<Gimnasio> Gimnasios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
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
                    .WithMany(p => p.AnunciosUsuarios)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_anuncios_usuarios_usuarios");
            });

            modelBuilder.Entity<Gimnasio>(entity =>
            {
                entity.ToTable("gimnasios");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.HasIndex(e => e.Cif, "gimnasios_cif_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Login, "login")
                    .IsUnique();

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

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("email")
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

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasColumnName("login")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("password")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci")
                    .HasConversion(new ValueConverter<Password, string>(
                        p => p.PasswordHash,
                        p => new Password {PasswordHash = p}));;

                entity.Property(e => e.Tarifa)
                    .HasColumnType("int(11)")
                    .HasColumnName("tarifa")
                    .HasComment("Es en céntimos");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.HasIndex(e => e.Login, "login")
                    .IsUnique();

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

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("email")
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

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("login")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("password")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci")
                    .HasConversion(new ValueConverter<Password, string>(
                        p => p.PasswordHash,
                        p => new Password {PasswordHash = p}));
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
