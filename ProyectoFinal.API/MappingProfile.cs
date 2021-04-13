using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using AutoMapper;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;
using System.Text;

namespace ProyectoFinal.API
{
    /// <summary>
    /// Clase que permite el mapeo automático desde los modelos de la base de datos a los DTOs que
    /// se usan en el resto de la aplicación
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <inheritdoc />
        public MappingProfile()
        {
            // AutoHash de Password para Gimnasios y Usuarios
            CreateMap<string, Password>().ConvertUsing(new HashConverter());

            //  ------- Gimnasios ----------

            // Mapeo Model --> DTO
            CreateMap<Gimnasio, GimnasioListaDto>();
            CreateMap<Gimnasio, GimnasioDetallesDto>();

            // Mapeo DTO   --> Model
            CreateMap<GimnasioLoginDto, Gimnasio>();
            CreateMap<GimnasioCreateDto, Gimnasio>();
            CreateMap<GimnasioUpdateDto, Gimnasio>()
                .ForMember(x => x.FechaCreado, opt => opt.Ignore());

            //  ------- Gimnasios ----------
            
            
            //  ------- Usuarios ----------

            // Mapeo Model --> DTO
            CreateMap<Usuario, UsuarioListaDto>();
            CreateMap<Usuario, UsuarioDetallesDto>();

            // Mapeo DTO   --> Model
            CreateMap<UsuarioLoginDto, Usuario>();
            CreateMap<UsuarioCreateDto, Usuario>();
            CreateMap<UsuarioUpdateDto, Usuario>()
                .ForMember(x => x.FechaCreado, opt => opt.Ignore());

            //  ------- Usuarios ----------
        }
    }

    internal class HashConverter : ITypeConverter<string, Password>
    {
        public Password Convert(string source, Password destination, ResolutionContext context)
        {
            return new()
            {
                PasswordHash = ToSha256(source)
            };
        }

        private static string ToSha256(string pass)
        {
            var sha256 = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass));
            var builder = new StringBuilder();
            foreach (var t in sha256)
                builder.Append(t.ToString("x2"));
            return builder.ToString();
        }
    }
}