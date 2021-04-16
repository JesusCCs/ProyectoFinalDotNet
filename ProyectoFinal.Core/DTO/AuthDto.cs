using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Core.DTO
{
    public class AuthDto
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required, StringLength(200, MinimumLength = 6)]
        public string Password { get; set; }

        [Required, EmailAddress, StringLength(200, MinimumLength = 6)]
        public string Email { get; set; }
    }
}