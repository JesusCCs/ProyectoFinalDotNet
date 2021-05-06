using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.API.Controllers
{
    [ApiController]
    [Route("/admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminController : ControllerBase
    {
        private readonly IAuthBl _authBl;
        private readonly IJwtTokenBl _jwtTokenBl;
        private readonly IWebHostEnvironment _env;

        public AdminController(IAuthBl authBl, IJwtTokenBl jwtTokenBl, IWebHostEnvironment env)
        {
            _authBl = authBl;
            _jwtTokenBl = jwtTokenBl;
            _env = env;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest itemLogin)
        {
            var guidAuth = await _authBl.Login(itemLogin, Rol.Admin);

            var token = _jwtTokenBl.GenerateAccessToken(guidAuth, guidAuth, Rol.Admin);
            var refreshToken = await _jwtTokenBl.GenerateRefreshToken(guidAuth);

            return Ok(new LoginResponse
            {
                Id = guidAuth,
                AuthId = guidAuth,
                AccessToken = token,
                RefreshToken = refreshToken
            });
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromForm] AuthBaseRequest request)
        {
            if (!_env.IsDevelopment())
            {
                return NotFound();
            }
            
            await _authBl.Create(request, Rol.Admin);
            return Ok();
        }
    }
}