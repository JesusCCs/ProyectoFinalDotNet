using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IAuthBl
    {
        Task<Guid> Create(SignUpBaseRequest signUpBaseRequest, string rol);
        Task<Guid> Login(LoginRequest signUpRequest,string rol);
        Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest);
        Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
    }
}