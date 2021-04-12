using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public sealed partial class Gimnasio : BaseAuth
    {
        public Gimnasio()
        {
            Anuncios = new HashSet<Anuncio>();
        }
        public string Cif { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Tarifa { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Anuncio> Anuncios { get; set; }
    }
}
