using System;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IAuthBl
    {
        Task<Guid> Create(AuthBaseRequest request, string rol);
        Task<Guid> Login(LoginRequest request,string rol);
        Task ForgotPassword(ForgotPasswordRequest request);
        Task ResetPassword(ResetPasswordRequest request);
        Task ChangePassword(ChangePasswordRequest request);
        Task ConfirmEmail(ConfirmEmailRequest request);
        Task ChangeEmail(ChangeEmailRequest request);
        Task ConfirmNewEmail(ConfirmNewEmailRequest request);
    }
}