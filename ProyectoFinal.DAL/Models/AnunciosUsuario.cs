using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.DAL.Models
{
    public partial class AnunciosUsuario
    {
        public int Id { get; set; }
        public int AnucioId { get; set; }
        public int UsuarioId { get; set; }
        public int Reproducciones { get; set; }
    }
}
