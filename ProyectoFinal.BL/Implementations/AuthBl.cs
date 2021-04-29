using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using FluentEmail.Core;
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
        private readonly IFluentEmail _email;
        private readonly IMapper _mapper;

        public AuthBl(UserManager<Auth> userManager, SignInManager<Auth> signInManager,IFluentEmail email, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _email = email;
            _mapper = mapper;
        }

        public async Task<Guid> Create(SignUpBaseRequest signUpBaseRequest, string rol)
        {
            var auth = _mapper.Map<Auth>(signUpBaseRequest);

            var result = await _userManager.CreateAsync(auth, signUpBaseRequest.Password);

            if (!result.Succeeded)
            {
                throw new UserCreationException();
            }

            await _userManager.AddToRoleAsync(auth, rol);

            return auth.Id;
        }

        public async Task<Guid> Login(LoginRequest loginRequest, string rol)
        {
            var isEmail = loginRequest.UserNameOrEmail.Contains("@");
            var auth = isEmail
                ? await _userManager.FindByEmailAsync(loginRequest.UserNameOrEmail)
                : await _userManager.FindByNameAsync(loginRequest.UserNameOrEmail);

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
                loginRequest.Password, loginRequest.RememberMe, auth.LockoutEnabled);

            if (result.Succeeded)
            {
                return auth.Id;
            }

            throw new AuthenticationException();
        }

        public async Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordRequest.Email);

            if (user == null)
            {
                return;
            }
                
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodeToken = HttpUtility.UrlEncode(token);
            
            var model = new 
            {
                Name = user.UserName,
                Token = encodeToken
            };
            
            await _email.To(user.Email).Subject("Contraseña olvidada")
                .UsingTemplateFromFile("wwwroot/templates/ResetPassword.cshtml",model)
                .SendAsync();
        }

        public async Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email);

            if (user == null)
            {
                throw new ResetPasswordException();
            }

            var decodeToken = HttpUtility.UrlDecode(resetPasswordRequest.Token);
            
            var result = await _userManager.ResetPasswordAsync(user, decodeToken, resetPasswordRequest.Password);

            if (!result.Succeeded)
            {
                throw new ResetPasswordException();
            }
        }
    }
}