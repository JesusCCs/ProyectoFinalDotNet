using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Core.DTO
{
    public class SignUpBaseRequest
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }
        
        [Required, EmailAddress, StringLength(200, MinimumLength = 6)]
        public string Email { get; set; }

        [Required, StringLength(200, MinimumLength = 6)]
        public string Password { get; set; }
    }
    
    public class LoginRequest
    {
        [Required, StringLength(200, MinimumLength = 3)]
        public string UserNameOrEmail { get; set; }

        [Required, StringLength(200, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
    
    public class LoginResponse
    {
        public Guid Id { get; init; }
        public Guid AuthId { get; init; }
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
    }
    
    public class ForgotPasswordRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
    
    public class ResetPasswordRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required, StringLength(200, MinimumLength = 6)]
        public string Password { get; set; }
        
        [Compare("Password")]
        public string ConfirmedPassword { get; set; }
        
        [Required]
        public string Token { get; set; }
    }
    
    public class ChangePasswordRequest
    {
        [Required]
        public Guid AuthId { get; set; }
        
        [Required, StringLength(200, MinimumLength = 6)]
        public string CurrentPassword { get; set; }
        
        [Required, StringLength(200, MinimumLength = 6)]
        public string NewPassword { get; set; }
        
        [Compare("NewPassword")]
        public string ConfirmedNewPassword { get; set; }
    }
    
    public class ChangeEmailRequest
    {
        [Required]
        public Guid AuthId { get; set; }
        
        [Required, EmailAddress]
        public string NewEmail { get; set; }
    }
    
    public class ConfirmEmailRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}