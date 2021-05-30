﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.API.Controllers
{
    [ApiController]
    [Route("/anuncios")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rol.Gimnasio)]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioBl _anuncioBl;

        public AnuncioController(IAnuncioBl anuncioBl)
        {
            _anuncioBl = anuncioBl;
        }
        
        [HttpPost]
        [Authorize(Roles = Rol.Gimnasio)]
        public async Task<ActionResult> Create([FromForm] AnuncioCreateRequest request)
        {
            var anuncio = await _anuncioBl.Create(request);
            return Ok(anuncio);
        }
        
        [HttpPut("/anuncios/{id:guid}/details")]
        [Authorize(Roles = Rol.Gimnasio)]
        public async Task<ActionResult> UpdateDetails(Guid id, [FromBody] AnuncioUpdateDetailsRequest request)
        {
            var anuncio = await _anuncioBl.UpdateDetails(id, request);
            return Ok(anuncio);
        }
        
        [HttpPut("/anuncios/{id:guid}/recurso")]
        [Authorize(Roles = Rol.Gimnasio)]
        public async Task<ActionResult> UpdateFile(Guid id, [FromForm] AnuncioUpdateRecursoRequest request)
        {
            var anuncio = await _anuncioBl.UpdateRecurso(id, request);
            return Ok(anuncio);
        }
        
        [HttpGet("/anuncios/{inicio:DateTime}/{fin:DateTime}")]
        [Authorize(Roles = Rol.Gimnasio)]
        public async Task<ActionResult> GetForCheck(DateTime inicio, DateTime fin)
        {
            var resultado = await _anuncioBl.CheckDates(inicio, fin);
            return Ok(resultado);
        }
    }
}