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

        public async Task<Guid> Create(SignUpBaseRequest request, string rol)
        {
            var auth = _mapper.Map<Auth>(request);

            var result = await _userManager.CreateAsync(auth, request.Password);

            if (!result.Succeeded)
            {
                throw new UserCreationException();
            }

            await _userManager.AddToRoleAsync(auth, rol);
            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(auth);
            var encodeToken = HttpUtility.UrlEncode(token);
            
            var model = new 
            {
                Name = auth.UserName,
                Token = encodeToken
            };
            
            await _email.To(auth.Email).Subject("Completar Registro")
                .UsingTemplateFromFile("wwwroot/templates/EmailConfimation.cshtml",model)
                .SendAsync();

            return auth.Id;
        }
        
        public async Task ConfirmEmail(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new ConfirmEmailException();
            }

            var decodeToken = HttpUtility.UrlDecode(request.Token);
            
            var result = await _userManager.ConfirmEmailAsync(user, decodeToken);

            if (!result.Succeeded)
            {
                throw new ConfirmEmailException();
            }
        }

        public async Task<Guid> Login(LoginRequest request, string rol)
        {
            var isEmail = request.UserNameOrEmail.Contains("@");
            var auth = isEmail
                ? await _userManager.FindByEmailAsync(request.UserNameOrEmail)
                : await _userManager.FindByNameAsync(request.UserNameOrEmail);

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
                request.Password, request.RememberMe, auth.LockoutEnabled);

            if (result.Succeeded)
            {
                return auth.Id;
            }

            throw new AuthenticationException();
        }

        public async Task ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return;
            }
                
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodeToken = HttpUtility.UrlEncode(token);
            
            var model = new 
            {
                Name = user.UserName,
                Url = encodeToken,
                Time = 60
            };
            
            await _email.To(user.Email).Subject("Contraseña olvidada")
                .UsingTemplateFromFile("wwwroot/templates/ResetPassword.cshtml",model)
                .SendAsync();
        }

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new ResetPasswordException();
            }

            var decodeToken = HttpUtility.UrlDecode(request.Token);
            
            var result = await _userManager.ResetPasswordAsync(user, decodeToken, request.Password);

            if (!result.Succeeded)
            {
                throw new ResetPasswordException();
            }
        }
    }
}