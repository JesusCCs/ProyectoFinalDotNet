using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.BL.Helpers;
using ProyectoFinal.Core;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.Core.Exceptions;
using ProyectoFinal.DAL.Models.Auth;
using static System.String;

namespace ProyectoFinal.BL.Implementations
{
    public class AuthBl : IAuthBl
    {
        private const string TemplateEmailPath = "ProyectoFinal.Core.Templates.{0}.cshtml";
        
        private readonly UserManager<Auth> _userManager;
        private readonly SignInManager<Auth> _signInManager;
        private readonly IFluentEmail _email;
        private readonly IMapper _mapper;
        private readonly FrontEnd _frontEnd;

        public AuthBl(UserManager<Auth> userManager, SignInManager<Auth> signInManager, IFluentEmail email,
            IMapper mapper, FrontEnd frontEnd)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _email = email;
            _mapper = mapper;
            _frontEnd = frontEnd;
        }

        public async Task<Guid> Create(AuthBaseRequest request, string rol)
        {
            var auth = _mapper.Map<Auth>(request);

            var result = await _userManager.CreateAsync(auth, request.Password);

            if (!result.Succeeded)
            {
                throw new UserCreationException(result.Errors);
            }

            await _userManager.AddToRoleAsync(auth, rol);

            // Si es administrador, verificamos automáticamente el correo
            if (rol == Rol.Admin)
            {
                auth.EmailConfirmed = true;
                await _userManager.UpdateAsync(auth);
                return auth.Id;
            }

            // En el caso de que no sea un administrador, se envía correo de confirmación
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(auth);
            var encodeToken = HttpUtility.UrlEncode(token);

            var model = new
            {
                Name = auth.UserName,
                Url = new Uri(_frontEnd.Url + "/auth/confirm-email").AddQuery("token", encodeToken).AddQuery("email",auth.Email),
                Time = App.TimeDefaultToken
            };

            await _email.To(auth.Email).Subject("Completar Registro")
                .UsingTemplateFromEmbedded(Format(TemplateEmailPath, EmailType.ConfirmEmail), model, typeof(App).Assembly)
                .SendAsync();

            return auth.Id;
        }
        
        public async Task ChangeEmail(ChangeEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.AuthId.ToString());

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
            var encodeToken = HttpUtility.UrlEncode(token);

            var model = new
            {
                Name = user.UserName,
                Url = encodeToken,
                Time = App.TimeDefaultToken
            };

            await _email.To(user.Email).Subject("Confirmar nuevo email")
                .UsingTemplateFromEmbedded(Format(TemplateEmailPath, EmailType.ConfirmNewEmail), model, typeof(App).Assembly)
                .SendAsync();
        }
        
        public async Task ConfirmEmail(ConfirmEmailRequest request)
        {
            var email = HttpUtility.UrlDecode(request.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var decodeToken = HttpUtility.UrlDecode(request.Token);

            var result = await _userManager.ConfirmEmailAsync(user, decodeToken);

            if (!result.Succeeded)
            {
                throw new ConfirmEmailException();
            }
        }
        
        public async Task ConfirmNewEmail(ConfirmNewEmailRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.CurrentEmail);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var decodeToken = HttpUtility.UrlDecode(request.Token);

            var result = await _userManager.ChangeEmailAsync(user, request.NewEmail, decodeToken);

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

            if (auth == null) throw new AuthenticationException();
            
            // Comprobamos que el usuario que se ha encontrado es del rol que toca
            var roles = await _userManager.GetRolesAsync(auth);

            if (roles.First() != rol) throw new AuthenticationException();
            
            var result = await _signInManager.PasswordSignInAsync(auth,
                request.Password, request.RememberMe, auth.LockoutEnabled);
            
            if (!result.Succeeded) throw new AuthenticationException();
            
            return auth.Id;
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
                Time = App.TimeDefaultToken
            };

            await _email.To(user.Email).Subject("Contraseña olvidada")
                .UsingTemplateFromEmbedded(Format(TemplateEmailPath, EmailType.ResetPassword), model, typeof(App).Assembly)
                .SendAsync();
        }

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var decodeToken = HttpUtility.UrlDecode(request.Token);

            var result = await _userManager.ResetPasswordAsync(user, decodeToken, request.Password);

            if (!result.Succeeded)
            {
                throw new ResetPasswordException();
            }
        }

        public async Task ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.AuthId.ToString());

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                throw new ChangePasswordException();
            }
        }
    }
}