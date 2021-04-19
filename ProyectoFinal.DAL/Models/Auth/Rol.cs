using System;
using Microsoft.AspNetCore.Identity;

namespace ProyectoFinal.DAL.Models.Auth
{
    public class Rol : IdentityRole<Guid>
    {
        public const string Gimnasio = "Gimnasio";
        public const string Usuario = "Usuario";
        public const string Admin = "Admin";
        public const string AdminOGimnasio = "Admin,Gimnasio";
        
    }
}