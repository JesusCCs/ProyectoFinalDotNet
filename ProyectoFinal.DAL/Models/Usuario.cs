using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public class Usuario : Base
    {
        public Usuario()
        {
            Anuncios = new HashSet<AnunciosUsuario>();
        }
        
        public Guid AuthId { get; set; }

        public virtual ICollection<AnunciosUsuario> Anuncios { get; set; }
        public Auth.Auth Auth { get; set; }
    }
}
