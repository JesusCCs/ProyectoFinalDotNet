using System;

namespace ProyectoFinal.Core.DTO
{
    public class GimnasioLoginDto
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    public class GimnasioCreateDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Cif { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public int Tarifa { get; set; }
    }
    
    public class GimnasioUpdateDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Cif { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public int Tarifa { get; set; }
    }
    
    public class GimnasioDetallesDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Cif { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreado { get; set; }
        public int Tarifa { get; set; }
    }
    
    public class GimnasioListaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Tarifa { get; set; }
    }
}