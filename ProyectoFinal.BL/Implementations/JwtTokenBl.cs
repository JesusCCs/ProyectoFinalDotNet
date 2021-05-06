using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.Core.Exceptions;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.BL.Implementations
{
    public class JwtTokenBl : IJwtTokenBl
    {
        private const string Algorithm = SecurityAlgorithms.HmacSha512Signature;
        
        private readonly JwtSettings _jwt;
        private readonly UserManager<Auth> _userManager;

        public JwtTokenBl(JwtSettings jwt, UserManager<Auth> userManager)
        {
            _jwt = jwt;
            _userManager = userManager;
        }

        public string GenerateAccessToken(Guid entityId, Guid authId, string rol)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, authId.ToString()),
                new(ClaimTypes.NameIdentifier, entityId.ToString()),
                new(ClaimTypes.Role, rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));
            var credentials = new SigningCredentials(key, Algorithm);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwt.AccessTokenExpirationInMinutes),
                Audience = _jwt.Audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var accessToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(accessToken);
        }

        public async Task<string> GenerateRefreshToken(Guid authId)
        {
            var auth = await _userManager.FindByIdAsync(authId.ToString());
            
            await _userManager.RemoveAuthenticationTokenAsync(auth, App.RefreshTokenProvider, App.RefreshToken);
            var refreshToken = await _userManager.GenerateUserTokenAsync(auth, App.RefreshTokenProvider, App.RefreshToken);
            
            await _userManager.SetAuthenticationTokenAsync(auth, App.RefreshTokenProvider, App.RefreshToken, refreshToken);

            return refreshToken;
        }

        public AccessTokenValidatedResponse ValidateAccessToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateLifetime = false, // en este caso no buscamos validar la caducidad
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret)),
                ValidAudience = _jwt.Audience
            }, out var validatedToken);

            if (validatedToken is not JwtSecurityToken token || token.Header.Alg != Algorithm)
            {
                throw new SecurityTokenException("Token no permitido");
            }
            
            var id = principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var authId = principal.Claims.Single(c => c.Type == ClaimTypes.Sid).Value;
            var rol = principal.Claims.Single(c => c.Type == ClaimTypes.Role).Value;

            if (new[] {id, authId, rol}.Any(s => s is null))
            {
                throw new SecurityTokenException("Token no permitido");
            }
            
            return new AccessTokenValidatedResponse
            {
                Id = id,
                AuthId = authId,
                Rol = rol
            };
        }

        public async Task<string> ValidateRefreshToken(string authId, string refreshToken)
        {
            var auth = await _userManager.FindByIdAsync(authId);

            if (auth is null)
            {
                throw new UserNotFoundException();
            }
            
            var refreshTokenSaved = await _userManager.GetAuthenticationTokenAsync(auth, App.RefreshTokenProvider, App.RefreshToken);

            if (refreshToken != refreshTokenSaved)
            {
                throw new SecurityTokenException("Refresh Token inválido");
            }

            var valid = await _userManager.VerifyUserTokenAsync(auth, App.RefreshTokenProvider, App.RefreshToken,refreshToken);
            
            if (!valid)
            {
                throw new SecurityTokenException("Refresh Token ha caducado");
            }

            return refreshToken;
        }
    }
}