using System;
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
        public async Task<ActionResult> UpdateCreation(Guid id, [FromBody] AnuncioUpdateDetailsRequest request)
        {
            var anuncio = await _anuncioBl.UpdateCreation(id, request);
            return Ok(anuncio);
        }
        
        [HttpPut("/anuncios/{id:guid}/recurso")]
        [Authorize(Roles = Rol.Gimnasio)]
        public async Task<ActionResult> UpdateFile(Guid id, [FromBody] AnuncioUpdateRecursoRequest request)
        {
            var anuncio = await _anuncioBl.UpdateRecurso(id, request);
            return Ok(anuncio);
        }
        
        [HttpGet]
        [Authorize(Roles = Rol.Gimnasio)]
        public async Task<ActionResult> GetForCheck([FromQuery] bool forCheckDates)
        {
            if (!forCheckDates) return NoContent();
            
            var lista = await _anuncioBl.GetDates();
            return Ok(lista);
        }
    }
}