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
            CreateMap<AuthBaseRequest, Auth>();

            //  ------- Gimnasios ----------

            // Mapeo Model --> DTO
            CreateMap<Gimnasio, GimnasioGetAllResponse>()
                .ForMember(destino => destino.Tarifa,
                opt => opt.MapFrom(src => src.Tarifa / 100.0f));;
            CreateMap<Gimnasio, GimnasioMobileResponse>()
                .ForMember(destino => destino.Tarifa,
                    opt => opt.MapFrom(src => src.Tarifa / 100.0f));
            
            CreateMap<Gimnasio, GimnasioGetByIdResponse>()
                .ForMember(destino => destino.Tarifa,
                    opt => opt.MapFrom(src => src.Tarifa / 100.0f))
                .ForMember(destino => destino.UserName,
                    opt => opt.MapFrom(src => src.Auth.UserName))
                .ForMember(destino => destino.Email,
                    opt => opt.MapFrom(src => src.Auth.Email));

            // Mapeo DTO   --> Model
            CreateMap<GimnasioCreateRequest, Gimnasio>()
                .ForMember(destino => destino.Tarifa,
                    opt => opt.MapFrom(src => src.Tarifa * 100));
            CreateMap<GimnasioCreateRequest, Auth>();
            CreateMap<GimnasioUpdateRequest, Gimnasio>()
                .ForMember(destino => destino.Tarifa,
                    opt => opt.MapFrom(src => src.Tarifa * 100))
                .ForMember(x => x.FechaCreado, 
                    opt => opt.Ignore())
                .ForMember(x => x.Auth, 
                    opt => opt.Ignore())
                .ForMember(x => x.FechaActualizado, 
                    opt => opt.Ignore());


            //  ------- Usuarios ----------

            // Mapeo Model --> DTO

            // Mapeo DTO   --> Model
            CreateMap<UsuarioCreateRequest, Usuario>();

            //  ------- Anuncios ----------

            // Mapeo Model --> DTO
            CreateMap<Anuncio, AnuncioDetallesResponse>();
            CreateMap<Anuncio, AnuncioDatesResponse>();
            CreateMap<Anuncio, AnuncioBaseResponse>();
            CreateMap<Anuncio, AnuncioToWatchResponse>();

            // Mapeo DTO   --> Model
            
        }
    }
}