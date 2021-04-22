
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.API.Controllers
{
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBl _authBl;
        private readonly IJwtTokenBl _jwtTokenBl;

        public AuthController(IAuthBl authBl, IJwtTokenBl jwtTokenBl)
        {
            _authBl = authBl;
            _jwtTokenBl = jwtTokenBl;
        }


        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest tokenRequest)
        {
            var infoUser = _jwtTokenBl.ValidateAccessToken(tokenRequest.AccessToken);
            var refreshToken = await _jwtTokenBl.ValidateRefreshToken(infoUser.AuthId, tokenRequest.RefreshToken);
            
            var accessToken = _jwtTokenBl.GenerateAccessToken(Guid.Parse(infoUser.Id), Guid.Parse(infoUser.AuthId), infoUser.Rol);

            return Ok(new RefreshTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}