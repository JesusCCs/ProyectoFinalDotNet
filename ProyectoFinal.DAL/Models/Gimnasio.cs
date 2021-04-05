using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public partial class Gimnasio
    {
        public int Id { get; set; }
        public int? AuthId { get; set; }
        public string Cif { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Tarifa { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime FechaActualizado { get; set; }
        public bool? Activo { get; set; }
    }
}
