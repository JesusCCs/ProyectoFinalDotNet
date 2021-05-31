using System;

namespace ProyectoFinal.Core.DTO
{

    public class UsuarioCreateRequest: AuthBaseRequest
    {
        public string Login { get; set; }
        public string ConfirmPassword { get; set; }
    }
}