using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.API;
using ProyectoFinal.BL.Contracts;

namespace ProyectoFinal.BL.Implementations
{
    public class JwtTokenBl : IJwtTokenBl
    {
        private readonly JwtSettings _jwt;

        public JwtTokenBl(JwtSettings jwt)
        {
            _jwt = jwt;
        }


        public string GenerateJwtToken(Guid entityId, Guid authId, string rol)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, authId.ToString()),
                new(ClaimTypes.NameIdentifier, entityId.ToString()),
                new(ClaimTypes.Role,rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(_jwt.AccessTokenExpirationInSeconds),
                Audience = _jwt.Audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var accessToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(accessToken);
        }
    }
}