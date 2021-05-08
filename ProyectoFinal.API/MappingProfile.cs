﻿using AutoMapper;
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
            CreateMap<Gimnasio, GimnasioGetAllResponse>();
            CreateMap<Gimnasio, GimnasioGetByIdResponse>();

            // Mapeo DTO   --> Model
            CreateMap<GimnasioCreateRequest, Gimnasio>();
            CreateMap<GimnasioCreateRequest, Auth>();
            CreateMap<GimnasioUpdateRequest, Gimnasio>()
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
            
            
            //  ------- Anuncios ----------
            
            // Mapeo Model --> DTO
            CreateMap<Anuncio, AnuncioGetByIdResponse>();
            CreateMap<Anuncio, AnuncioCheckAllResponse>();
            
            // Mapeo DTO   --> Model
            CreateMap<AnuncioCreateRequest, Anuncio>();
        }
    }
}