using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.BL.Exceptions;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.BL.Implementations
{
    public class AuthBl : IAuthBl
    {
        private readonly UserManager<Auth> _userManager;
        private readonly SignInManager<Auth> _signInManager;
        private readonly IMapper _mapper;

        public AuthBl(UserManager<Auth> userManager, SignInManager<Auth> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<Guid> Create(AuthSignUpDto authSignUpDto, string rol)
        {
            var auth = _mapper.Map<Auth>(authSignUpDto);

            var result = await _userManager.CreateAsync(auth, authSignUpDto.Password);

            if (!result.Succeeded)
            {
                throw new UserCreationException();
            }

            await _userManager.AddToRoleAsync(auth, rol);

            return auth.Id;
        }

        public async Task<Guid> Login(AuthLoginDto authLoginDto, string rol)
        {
            var isEmail = authLoginDto.UserNameOrEmail.Contains("@");
            var auth = isEmail
                ? await _userManager.FindByEmailAsync(authLoginDto.UserNameOrEmail)
                : await _userManager.FindByNameAsync(authLoginDto.UserNameOrEmail);

            if (auth == null)
            {
                throw new AuthenticationException();
            }
            
            // Comprobamos que el usuario que se ha encontrado es del rol que toca
            var roles = await _userManager.GetRolesAsync(auth);

            if (roles.First() != rol)
            {
                throw new AuthenticationException();
            }
            
            var result = await _signInManager.PasswordSignInAsync(auth,
                authLoginDto.Password, authLoginDto.RememberMe, auth.LockoutEnabled);

            if (result.Succeeded)
            {
                return auth.Id;
            }

            throw new AuthenticationException();
        }
    }
}