using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public class Gimnasio : Base
    {
        public Gimnasio()
        {
            Anuncios = new HashSet<Anuncio>();
        }
        
        public Guid AuthId { get; set; }
        
        public string Cif { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Tarifa { get; set; }
        public string Descripcion { get; set; }
        
        public ICollection<Anuncio> Anuncios { get; set; }
        public Auth.Auth Auth { get; set; }
    }
}
