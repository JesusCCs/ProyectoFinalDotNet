﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.Core.Validation;

namespace ProyectoFinal.Core.DTO
{
    public class GimnasioCreateRequest : AuthBaseRequest
    {
        [Required, StringLength(9)] 
        public string Cif { get; set; }

        [Required, StringLength(200, MinimumLength = 6)]
        public string Nombre { get; set; }

        [Required, StringLength(200, MinimumLength = 5)]
        public string Direccion { get; set; }

        [Required, MinLength(10)] 
        public string Descripcion { get; set; }

        [Required, Range(1, 9999)] 
        public float Tarifa { get; set; }
        
        [MaxFileSize(2), AllowedMimeType(".jpg,.jpeg,.png")]
        public IFormFile Logo { get; set; }
    }

    public class GimnasioUpdateRequest
    {
        [Required] 
        public Guid Id { get; set; }
        
        [Required, StringLength(9)] 
        public string Cif { get; set; }

        [Required, StringLength(200, MinimumLength = 6)]
        public string Nombre { get; set; }
        
        [Required, StringLength(200, MinimumLength = 5)]
        public string Direccion { get; set; }

        [Required, MinLength(10)] 
        public string Descripcion { get; set; }

        [Required, Range(1, 9999)] 
        public float Tarifa { get; set; }
        
        [MaxFileSize(2), AllowedMimeType(".jpg,.jpeg,.png")]
        public IFormFile Logo { get; set; }
    }

    public class GimnasioGetByIdResponse
    {
        public Guid Id { get; set; }
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Identificador { get; set; }
        
        public string Cif { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public string Logo { get; set; }
        
        public float Tarifa { get; set; }
        
        public DateTime FechaCreado { get; set; }
        
        public bool RecibidoTour { get; set; }
    }

    public class GimnasioGetAllResponse
    {
        public Guid Id { get; set; }
        
        public string Logo { get; set; }
        
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        public int Tarifa { get; set; }
    }
}