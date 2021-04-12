using System.Security;
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
            CreateMap<GimnasioUpdateDto, Gimnasio>();
            
            //  ------- Gimnasios ----------
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