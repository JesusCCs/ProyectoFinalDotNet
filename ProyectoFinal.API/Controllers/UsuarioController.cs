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
    [Route("/usuarios")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController: ControllerBase
    {
        private readonly IAuthBl _authBl;
        private readonly IUsuarioBl _usuarioBl;
        private readonly IAnuncioBl _anuncioBl;
        private readonly IJwtTokenBl _jwtTokenBl;

        public UsuarioController(IAuthBl authBl, IUsuarioBl usuarioBl, IAnuncioBl anuncioBl, IJwtTokenBl jwtTokenBl)
        {
            _authBl = authBl;
            _usuarioBl = usuarioBl;
            _anuncioBl = anuncioBl;
            _jwtTokenBl = jwtTokenBl;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest itemLogin)
        {
            var guidAuth = await _authBl.Login(itemLogin, Rol.Usuario);

            var id = await _usuarioBl.GetIdByAuthId(guidAuth);

            var token = _jwtTokenBl.GenerateAccessToken(id, guidAuth, Rol.Usuario);
            var refreshToken = await _jwtTokenBl.GenerateRefreshToken(guidAuth);

            return Ok(new LoginResponse
            {
                Id = id,
                AuthId = guidAuth,
                AccessToken = token,
                RefreshToken = refreshToken
            });
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromBody] UsuarioCreateRequest request)
        {
            var guid = await _authBl.Create(request, Rol.Usuario);
            await _usuarioBl.Create(request, guid);
            
            return Ok();
        }
    }
}