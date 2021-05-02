using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IAuthBl
    {
        Task<Guid> Create(SignUpBaseRequest request, string rol);
        Task<Guid> Login(LoginRequest request,string rol);
        Task ForgotPassword(ForgotPasswordRequest request);
        Task ResetPassword(ResetPasswordRequest request);
        Task ConfirmEmail(ConfirmEmailRequest request);
        Task ChangePassword(ChangePasswordRequest request);
    }
}