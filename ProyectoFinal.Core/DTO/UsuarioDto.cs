using System;

namespace ProyectoFinal.Core.DTO
{
    public class UsuarioLoginDto
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UsuarioCreateDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
    }

    public class UsuarioUpdateDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
    }

    public class UsuarioDetallesDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCreado { get; set; }
    }

    public class UsuarioListaDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaCreado { get; set; }
        public bool Activo { get; set; }
    }
}