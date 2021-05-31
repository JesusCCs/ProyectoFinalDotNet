using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public class Anuncio : Base
    {
        public Anuncio()
        {
            AnunciosUsuarios = new HashSet<AnunciosUsuario>();
        }
        
        public Guid GimnasioId { get; init; }
        
        public bool Finalizado { get; set; }
        
        public string Tipo { get; set; }
        public string Recurso { get; set; }
        
        public DateTime? Inicio { get; set; }
        public DateTime? Fin { get; set; }
        
        public int? ReproduccionesLimite { get; set; }
        
        public Gimnasio Gimnasio { get; set; }
        public ICollection<AnunciosUsuario> AnunciosUsuarios { get; set; }
    }
}
