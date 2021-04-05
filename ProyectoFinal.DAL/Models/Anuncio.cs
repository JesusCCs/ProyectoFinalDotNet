using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public partial class Anuncio
    {
        public int Id { get; set; }
        public int? GimnasioId { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int ReproduccionesLimite { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime FechaActualizado { get; set; }
        public bool? Activo { get; set; }
    }
}
