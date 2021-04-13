using System;

namespace ProyectoFinal.DAL.Models
{
    public class Base
    {
        public Guid Id { get; set; }
        public DateTime? FechaCreado { get; set; }
        public DateTime FechaActualizado { get; set; }
        public bool? Activo { get; set; }
    }
}