using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Core.DTO
{
    public class GimnasioCreateDto : AuthSignUpDto
    {
        [Required, StringLength(9)] public string Cif { get; set; }

        [Required, StringLength(200, MinimumLength = 6)]
        public string Nombre { get; set; }

        [Required, StringLength(200, MinimumLength = 5)]
        public string Direccion { get; set; }

        [Required] [MinLength(10)] public string Descripcion { get; set; }

        [Required] [Range(100, 50000)] public int Tarifa { get; set; }
    }

    public class GimnasioUpdateDto
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Cif { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public string Direccion { get; set; }
        [Required] public string Descripcion { get; set; }
        [Required] public int Tarifa { get; set; }
    }

    public class GimnasioDetallesDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
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