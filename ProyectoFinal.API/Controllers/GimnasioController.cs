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
    [Route("/gimnasio")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GimnasioController : ControllerBase
    {
        private readonly IGinmasioBl _gimnasioBl;
        private readonly IAuthBl _authBl;
        private readonly IJwtTokenBl _jwtTokenBl;

        public GimnasioController(IGinmasioBl gimnasioBl, IAuthBl authBl, IJwtTokenBl jwtTokenBl)
        {
            _gimnasioBl = gimnasioBl;
            _authBl = authBl;
            _jwtTokenBl = jwtTokenBl;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/gimnasio/login")]
        public async Task<ActionResult> Login([FromBody] AuthLoginDto itemLogin)
        {
            var guidAuth = await _authBl.Login(itemLogin, Rol.Gimnasio);

            var gimansio = await _gimnasioBl.GetByAuthId(guidAuth);

            var token = _jwtTokenBl.GenerateAccessToken(gimansio.Id, guidAuth, Rol.Gimnasio);
            var refreshToken = await _jwtTokenBl.GenerateRefreshToken(guidAuth);

            return Ok(new AuthLoginResponseDto
            {
                Id = gimansio.Id,
                AuthId = guidAuth,
                AccessToken = token,
                RefreshToken = refreshToken
            });
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


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromBody] GimnasioCreateDto itemNuevo)
        {
            var guid = await _authBl.Create(itemNuevo, Rol.Gimnasio);

            var item = await _gimnasioBl.Create(itemNuevo, (Guid) guid);

            return Ok(item);
        }


        [HttpPut("{id:guid}")]
        [Authorize(Roles = Rol.AdminOGimnasio, Policy = Policy.GymIsOwner)]
        public async Task<ActionResult> Update(Guid id, [FromBody] GimnasioUpdateDto itemActualizado)
        {
            var item = await _gimnasioBl.Update(id, itemActualizado);
            return Ok(item);
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