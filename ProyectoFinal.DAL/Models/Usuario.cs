using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public int? AuthId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime FechaActualizado { get; set; }
        public bool? Activo { get; set; }
    }
}
