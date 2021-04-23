using System;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public class AnunciosUsuario : Base
    {
        public Guid AnucioId { get; set; }
        public Guid UsuarioId { get; set; }
        public int Reproducciones { get; set; }

        public virtual Anuncio Anucio { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
