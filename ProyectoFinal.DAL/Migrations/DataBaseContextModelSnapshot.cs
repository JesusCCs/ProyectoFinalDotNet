﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoFinal.DAL;

namespace ProyectoFinal.DAL.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("role_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("AuthId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("auth_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("AuthId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("auth_login");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("AuthId");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("auth_roles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                            RoleId = new Guid("893feaeb-af9f-400c-8779-4c783f3986b4")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("AuthId");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("auth_tokens");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Anuncio", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("activo")
                        .HasDefaultValueSql("'1'");

                    b.Property<DateTime>("FechaActualizado")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaActualizado")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("FechaCreado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaCreado")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("Fin")
                        .HasColumnType("datetime")
                        .HasColumnName("fin");

                    b.Property<bool>("Finalizado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("finalizado")
                        .HasDefaultValueSql("'0'")
                        .HasComment("Nos indica si este registro es de un anuncio cuya creación ha sido finalizada o no");

                    b.Property<Guid>("GimnasioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("gimnasioID")
                        .HasDefaultValueSql("''")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<DateTime?>("Inicio")
                        .HasColumnType("datetime")
                        .HasColumnName("inicio");

                    b.Property<string>("Recurso")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("recurso")
                        .HasComment("Se guarda la referencia al archivo en la carpeta anuncios");

                    b.Property<int?>("Reproducciones")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)")
                        .HasColumnName("reproducciones")
                        .HasDefaultValueSql("'0'");

                    b.Property<int?>("ReproduccionesLimite")
                        .HasColumnType("int(10)")
                        .HasColumnName("reproduccionesLimite");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("enum('imagen','gif','video')")
                        .HasColumnName("tipo")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "GimnasioId" }, "FK_anuncios_gimnasios");

                    b.ToTable("anuncios");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.AnunciosUsuario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("activo")
                        .HasDefaultValueSql("'1'");

                    b.Property<Guid>("AnucioId")
                        .HasColumnType("char(36)")
                        .HasColumnName("anucioID")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<DateTime>("FechaActualizado")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaActualizado")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("FechaCreado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaCreado")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("Reproducciones")
                        .HasColumnType("int(2)")
                        .HasColumnName("reproducciones");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("char(36)")
                        .HasColumnName("usuarioID")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AnucioId" }, "FK_anuncios_usuarios_anuncios");

                    b.HasIndex(new[] { "UsuarioId" }, "FK_anuncios_usuarios_usuarios");

                    b.ToTable("anuncios_usuarios");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Auth.Auth", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("auth");

                    b.HasData(
                        new
                        {
                            Id = new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2bb982a3-125a-4cf2-99d9-9afe23955967",
                            Email = "admin@email.es",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "admin@email.es",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEC0mBUacSKuceLsKg4kUkEfzsonfn2oNxWtMPVffJOYmDkuRnojNNQGKd61yVVwQYA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "43d40475-3d3b-4c70-aa32-a61cc8e4ef32",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Auth.Rol", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                            ConcurrencyStamp = "2fb8cd29-c056-48cb-902f-7a99e93f3664",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("20889caa-227f-42a3-98ac-ab26f51fab17"),
                            ConcurrencyStamp = "1fb0b440-425a-4a90-b9f4-3aa7b058e83a",
                            Name = "Gimnasio",
                            NormalizedName = "GIMNASIO"
                        },
                        new
                        {
                            Id = new Guid("99098188-e618-4c4c-a756-6f70ddeb2628"),
                            ConcurrencyStamp = "7a85a986-98aa-4bb2-909b-656e2e964bb7",
                            Name = "Usuario",
                            NormalizedName = "USUARIO"
                        });
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Gimnasio", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("activo")
                        .HasDefaultValueSql("'1'");

                    b.Property<Guid>("AuthId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Cif")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasColumnName("cif")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descripcion")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("direccion")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<DateTime>("FechaActualizado")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaActualizado")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("FechaCreado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaCreado")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Identificador")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("identificador")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<string>("Logo")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("logo")
                        .HasComment("Campo útil para tener una referencia al nombre con el que se guardó el fichero subido por el usuario en wwwroot");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nombre")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<bool>("RecibidoTour")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("recibido_tour")
                        .HasDefaultValueSql("'0'");

                    b.Property<int>("Tarifa")
                        .HasColumnType("int(11)")
                        .HasColumnName("tarifa")
                        .HasComment("Es en céntimos");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AuthId" }, "FK_auth_gimnasios");

                    b.ToTable("gimnasios");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id")
                        .UseCollation("utf8mb4_unicode_ci")
                        .HasCharSet("utf8mb4");

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("activo")
                        .HasDefaultValueSql("'1'");

                    b.Property<Guid>("AuthId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("FechaActualizado")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaActualizado")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("FechaCreado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaCreado")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("AuthId");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("ProyectoFinal.DAL.Models.Auth.Rol", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("ProyectoFinal.DAL.Models.Auth.Auth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("ProyectoFinal.DAL.Models.Auth.Auth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("ProyectoFinal.DAL.Models.Auth.Rol", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoFinal.DAL.Models.Auth.Auth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("ProyectoFinal.DAL.Models.Auth.Auth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Anuncio", b =>
                {
                    b.HasOne("ProyectoFinal.DAL.Models.Gimnasio", "Gimnasio")
                        .WithMany("Anuncios")
                        .HasForeignKey("GimnasioId")
                        .HasConstraintName("FK_anuncios_gimnasios")
                        .IsRequired();

                    b.Navigation("Gimnasio");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.AnunciosUsuario", b =>
                {
                    b.HasOne("ProyectoFinal.DAL.Models.Anuncio", "Anucio")
                        .WithMany("AnunciosUsuarios")
                        .HasForeignKey("AnucioId")
                        .HasConstraintName("FK_anuncios_usuarios_anuncios")
                        .IsRequired();

                    b.HasOne("ProyectoFinal.DAL.Models.Usuario", "Usuario")
                        .WithMany("Anuncios")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK_anuncios_usuarios_usuarios")
                        .IsRequired();

                    b.Navigation("Anucio");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Gimnasio", b =>
                {
                    b.HasOne("ProyectoFinal.DAL.Models.Auth.Auth", "Auth")
                        .WithMany()
                        .HasForeignKey("AuthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auth");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Usuario", b =>
                {
                    b.HasOne("ProyectoFinal.DAL.Models.Auth.Auth", "Auth")
                        .WithMany()
                        .HasForeignKey("AuthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auth");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Anuncio", b =>
                {
                    b.Navigation("AnunciosUsuarios");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Gimnasio", b =>
                {
                    b.Navigation("Anuncios");
                });

            modelBuilder.Entity("ProyectoFinal.DAL.Models.Usuario", b =>
                {
                    b.Navigation("Anuncios");
                });
#pragma warning restore 612, 618
        }
    }
}
