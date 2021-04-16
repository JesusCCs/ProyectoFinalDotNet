using AutoMapper;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.API
{
    /// <summary>
    /// Clase que permite el mapeo automático desde los modelos de la base de datos a los DTOs que
    /// se usan en el resto de la aplicación y viceversa
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //  ---------  Auth  -----------
            CreateMap<AuthDto, Auth>();
            
            //  ------- Gimnasios ----------

            // Mapeo Model --> DTO
            CreateMap<Gimnasio, GimnasioListaDto>();
            CreateMap<Gimnasio, GimnasioDetallesDto>();

            // Mapeo DTO   --> Model
            CreateMap<GimnasioCreateDto, Gimnasio>();
            CreateMap<GimnasioCreateDto, Auth>();
            CreateMap<GimnasioUpdateDto, Gimnasio>()
                .ForMember(x => x.FechaCreado, opt => opt.Ignore());


            //  ------- Usuarios ----------

            // Mapeo Model --> DTO
            CreateMap<Usuario, UsuarioListaDto>();
            CreateMap<Usuario, UsuarioDetallesDto>();

            // Mapeo DTO   --> Model
            CreateMap<UsuarioLoginDto, Usuario>();
            CreateMap<UsuarioCreateDto, Usuario>();
            CreateMap<UsuarioUpdateDto, Usuario>()
                .ForMember(x => x.FechaCreado, opt => opt.Ignore());
        }
    }
}