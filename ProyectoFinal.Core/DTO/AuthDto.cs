﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Core.DTO
{
    public class AuthSignUpDto
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }
        
        [Required, EmailAddress, StringLength(200, MinimumLength = 6)]
        public string Email { get; set; }

        [Required, StringLength(200, MinimumLength = 6)]
        public string Password { get; set; }
    }
    
    public class AuthLoginDto
    {
        [Required, StringLength(200, MinimumLength = 3)]
        public string UserNameOrEmail { get; set; }

        [Required, StringLength(200, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
    
    public class AuthLoginResponseDto
    {
        public Guid Id { get; init; }
        public Guid AuthId { get; init; }
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
    }
}