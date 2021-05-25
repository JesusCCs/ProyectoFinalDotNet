using System;
using System.Collections.Generic;

namespace ProyectoFinal.DAL.Models
{
    public class Gimnasio : Base
    {
        public Gimnasio()
        {
            Anuncios = new HashSet<Anuncio>();
            Auth = new Auth.Auth();
        }
        
        public Guid AuthId { get; set; }
        
        public string Cif { get; set; }
        public string Identificador { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Tarifa { get; set; }
        public string Descripcion { get; set; }
        public string Logo { get; set; }
        public bool RecibidoTour { get; set; }
        
        public ICollection<Anuncio> Anuncios { get; set; }
        public Auth.Auth Auth { get; set; }
    }
}
