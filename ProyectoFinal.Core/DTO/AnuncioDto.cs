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

        [Required, StringLength(200, MinimumLength = 6)]
        public int ReproduccionesLimite { get; set; }

        [Required, StringLength(200, MinimumLength = 5)]
        public DateTime FechaInicio { get; set; }

        [Required, MinLength(10)] 
        public DateTime FechaFin { get; set; }
            
        [Required, MaxFileSize(30), AllowedMimeType(".jpg,.jpeg,.png,.gif,.mp4,.avi,.webm")]
        public IFormFile Recurso { get; set; }
    }
        
    public class AnuncioGetByIdResponse
    {
        public Guid Id { get; set; }
            
        public int ReproduccionesLimite { get; set; }
            
        public DateTime FechaInicio { get; set; }
            
        public DateTime FechaFin { get; set; }
            
        public string Recurso { get; set; }
    }
        
    public class AnuncioCheckAllResponse
    {
        public DateTime FechaInicio { get; set; }
            
        public DateTime FechaFin { get; set; }
    }
}