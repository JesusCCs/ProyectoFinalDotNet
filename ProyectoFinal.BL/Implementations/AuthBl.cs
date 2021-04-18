using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.API;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.BL.Implementations
{
    public class AuthBl : IAuthBl
    {
        private readonly UserManager<Auth> _userManager;
        private readonly SignInManager<Auth> _signInManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwt;

        public AuthBl(UserManager<Auth> userManager, IMapper mapper, SignInManager<Auth> signInManager, JwtSettings jwt)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _jwt = jwt;
        }

        public async Task<Guid?> SignUp(AuthSignUpDto authSignUpDto, string rol)
        {
            var auth = _mapper.Map<Auth>(authSignUpDto);

            var result = await _userManager.CreateAsync(auth, authSignUpDto.Password);

            if (!result.Succeeded)
            {
                return null;
            }

            await _userManager.AddToRoleAsync(auth, rol);

            return auth.Id;
        }

        public async Task<Guid?> SignIn(AuthSignInDto authSignInDto)
        {
            var esEmail = authSignInDto.UserNameOrEmail.Contains("@");
            var auth = esEmail
                ? await _userManager.FindByEmailAsync(authSignInDto.UserNameOrEmail)
                : await _userManager.FindByNameAsync(authSignInDto.UserNameOrEmail);

            if (auth == null)
            {
                return null;
            }

            var result = await _signInManager.PasswordSignInAsync(auth,
                authSignInDto.Password, authSignInDto.RememberMe, auth.LockoutEnabled);

            if (result.Succeeded)
            {
                return auth.Id;
            }

            return null;
        }

        private async Task<string> GenerateJwtToken(Auth auth)
        {
            var roles = await _userManager.GetRolesAsync(auth);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, auth.Id.ToString()),
                // Solo hay un solo rol por cada usuario, luego con quedarnos con el primero es suficiente
                new(ClaimTypes.Role, roles.First())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(_jwt.AccessTokenExpiration),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}