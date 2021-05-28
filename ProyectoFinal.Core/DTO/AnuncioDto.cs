﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.Core.Validation;

namespace ProyectoFinal.Core.DTO
{
    public class AnuncioCreateRequest
    {
        [Required] 
        public Guid GimnasioId { get; set; }

        [Required]
        public int ReproduccionesLimite { get; set; }

        [Required]
        public DateTime Inicio { get; set; }

        [Required] 
        public DateTime Fin { get; set; }
            
        [Required, MaxFileSize(30), AllowedMimeType(".jpg,.jpeg,.png,.gif,.mp4,.avi,.webm")]
        public IFormFile Recurso { get; set; }
    }
        
    public class AnuncioDetallesResponse
    {
        public Guid Id { get; set; }
            
        public int ReproduccionesLimite { get; set; }
            
        public DateTime Inicio { get; set; }
            
        public DateTime Fin { get; set; }
            
        public string Recurso { get; set; }
    }
        
    public class AnuncioDatesResponse
    {
        public DateTime Inicio { get; set; }
            
        public DateTime Fin { get; set; }
    }
}