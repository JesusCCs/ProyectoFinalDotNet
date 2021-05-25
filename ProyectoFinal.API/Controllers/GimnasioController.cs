using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.API.Controllers
{
    [ApiController]
    [Route("/gimnasios")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GimnasioController : ControllerBase
    {
        private readonly IAuthBl _authBl;
        private readonly IGinmasioBl _gimnasioBl;
        private readonly IJwtTokenBl _jwtTokenBl;

        public GimnasioController(IGinmasioBl gimnasioBl, IAuthBl authBl, IJwtTokenBl jwtTokenBl)
        {
            _authBl = authBl;
            _jwtTokenBl = jwtTokenBl;
            _gimnasioBl = gimnasioBl;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest itemLogin)
        {
            var guidAuth = await _authBl.Login(itemLogin, Rol.Gimnasio);

            var gimansio = await _gimnasioBl.GetByAuthId(guidAuth);

            var token = _jwtTokenBl.GenerateAccessToken(gimansio.Id, guidAuth, Rol.Gimnasio);
            var refreshToken = await _jwtTokenBl.GenerateRefreshToken(guidAuth);

            return Ok(new LoginResponse
            {
                Id = gimansio.Id,
                AuthId = guidAuth,
                AccessToken = token,
                RefreshToken = refreshToken
            });
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromForm] GimnasioCreateRequest request)
        {
            var guid = await _authBl.Create(request, Rol.Gimnasio);
            await _gimnasioBl.Create(request, guid);
            
            return Ok();
        }
        
        [HttpGet]
        [Authorize(Roles = Rol.Admin)]
        public async Task<ActionResult> GetAll()
        {
            var lista = await _gimnasioBl.GetAll();
            return Ok(lista);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var item = await _gimnasioBl.GetById(id);
            return Ok(item);
        }
        
        [HttpPut("{id:guid}")]
        [Authorize(Roles = Rol.AdminOGimnasio, Policy = Policy.GymIsTarget)]
        public async Task<ActionResult> Update(Guid id, [FromForm] GimnasioUpdateRequest request)
        {
            await _gimnasioBl.Update(id, request);
            return NoContent();
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = Rol.Admin)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var item = await _gimnasioBl.Delete(id);
            return Ok(item);
        }
    }
}