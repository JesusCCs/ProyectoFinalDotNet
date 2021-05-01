
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.API.Controllers
{
    [ApiController]
    [Route("/auth")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var infoUser = _jwtTokenBl.ValidateAccessToken(request.AccessToken);
            await _jwtTokenBl.ValidateRefreshToken(infoUser.AuthId, request.RefreshToken);
            
            var accessToken = _jwtTokenBl.GenerateAccessToken(Guid.Parse(infoUser.Id), Guid.Parse(infoUser.AuthId), infoUser.Rol);
            var refreshToken = await _jwtTokenBl.GenerateRefreshToken(Guid.Parse(infoUser.AuthId));

            return Ok(new RefreshTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        
        [HttpPut]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _authBl.ForgotPassword(request);
            return Accepted();
        }
        
        [HttpPut]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            await _authBl.ResetPassword(request);
            return NoContent();
        }
        
        [HttpPut]
        [AllowAnonymous]
        [Route("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            await _authBl.ConfirmEmail(request);
            return Accepted();
        }
    }
}