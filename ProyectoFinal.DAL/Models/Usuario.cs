using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public partial class Usuario : BaseAuth
    {
        public Usuario()
        {
            AnunciosUsuarios = new HashSet<AnunciosUsuario>();
        }
        
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public virtual ICollection<AnunciosUsuario> AnunciosUsuarios { get; set; }
    }
}
