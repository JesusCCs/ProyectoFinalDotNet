using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IAuthBl
    {
        Task<Guid?> SignUp(AuthSignUpDto authSignUpDto, string rol);
        Task<Guid?> SignIn(AuthSignInDto authSignUpDto);
    }
}