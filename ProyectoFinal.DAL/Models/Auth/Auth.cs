using System;
using Microsoft.AspNetCore.Identity;

namespace ProyectoFinal.DAL.Models.Auth
{
    public class Auth : IdentityUser<Guid>
    {
        public Auth() : base() { }
    }
}