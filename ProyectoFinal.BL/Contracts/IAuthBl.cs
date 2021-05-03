using System;
using System.Threading.Tasks;
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
        Task ChangeEmail(ChangeEmailRequest request);
    }
}