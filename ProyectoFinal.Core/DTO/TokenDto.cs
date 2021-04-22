
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Core.DTO
{
    public class RefreshTokenRequest
    {
        [Required]
        public string AccessToken { get; set; }
        
        [Required]
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenResponse
    {
        public string AccessToken { get; init; }
        
        public string RefreshToken { get; init; }
    }
    
    public class AccessTokenValidatedResponse
    {
        public string Id { get; init; }
        
        public string AuthId { get; init; }
        
        public string Rol { get; init; }
    }
}