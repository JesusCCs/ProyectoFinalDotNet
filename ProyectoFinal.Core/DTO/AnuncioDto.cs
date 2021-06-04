using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.Core.Validation;

namespace ProyectoFinal.Core.DTO
{
    public class AnuncioCreateRequest
    {
        [Required] 
        public Guid GimnasioId { get; set; }

        [Required, MaxFileSize(30), AllowedMimeType(".jpg,.jpeg,.png,.gif,.mp4,.avi,.webm")]
        public IFormFile Recurso { get; set; }
    }
    
    public class AnuncioUpdateRecursoRequest : AnuncioCreateRequest
    {
        
    }

    public class AnuncioConfirmRequest
    {
        [Required] 
        public bool Finalizado { get; set; }
    }
    
    public class AnuncioUpdateRequest
    {
        [Required] 
        public bool Activo { get; set; }
    }

    public class AnuncioUpdateDetailsRequest
    {
        [Required]
        public int ReproduccionesLimite { get; set; }
        
        [Required] 
        public DateTime Inicio { get; set; }
            
        [Required]
        public DateTime Fin { get; set; }
    }
        
    public class AnuncioDetallesResponse: AnuncioBaseResponse
    {
        
    }
    
    public class AnuncioBaseResponse
    {
        public Guid Id { get; set; }
            
        public int ReproduccionesLimite { get; set; }
        
        public int Reproducciones { get; set; }
        
        public string Tipo { get; set; }
            
        public DateTime Inicio { get; set; }
            
        public DateTime Fin { get; set; }
        
        public bool Activo { get; set; }
            
        public string Recurso { get; set; }
    }
        
    public class AnuncioDatesResponse
    {
        public DateTime Inicio { get; set; }
            
        public DateTime Fin { get; set; }
    }
    
    public class AnuncioToWatchResponse
    {
        public Guid Id { get; set; }
        public string Recurso { get; set; }
        public string Tipo { get; set; }
    }
    
    // En caso de que se quiera limitar las reproducciones que quiere ver un usuario en particular
    public class AnuncioWatchedRequest
    {
        [Required]
        public Guid UsuarioId { get; set; }
    }
}