using System;

namespace ProyectoFinal.Core.DTO
{
    public class GimnasioDto
    {
        public int Id { get; set; }
        public AuthDto Auth { get; set; }
        public string Cif { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreado { get; set; }
        public int Tarifa { get; set; }
    }
}