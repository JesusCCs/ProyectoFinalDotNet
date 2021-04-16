using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IAuthBl
    {
        Task<Guid?> SignUp(AuthDto authDto, string rol);
        Task<bool> SignIn(AuthDto authDto);
    }
}