﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public partial class Anuncio : Base
    {
        public Anuncio()
        {
            AnunciosUsuarios = new HashSet<AnunciosUsuario>();
        }
        
        public Guid GimnasioId { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int ReproduccionesLimite { get; set; }
        public Gimnasio Gimnasio { get; set; }
        public ICollection<AnunciosUsuario> AnunciosUsuarios { get; set; }
    }
}
