using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ProyectoFinal.Core.Validation
{
    public class AllowedMimeTypeAttribute : ValidationAttribute
    {
        private readonly string[] _mimesAllowed;

        public AllowedMimeTypeAttribute(string mimeTypesAllowed)
        {
            _mimesAllowed = mimeTypesAllowed.Split(",");
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not IFormFile file) return ValidationResult.Success;

            var extension = Path.GetExtension(file.FileName);

            return !_mimesAllowed.Contains(extension)
                ? new ValidationResult(ErrorMessage(extension))
                : ValidationResult.Success;
        }

        private new string ErrorMessage(string extension)
        {
            return
                $"La extensión '{extension}' del archivo no es permitida. Tiene que ser una de las siguientes: {string.Join(", ", _mimesAllowed)}";
        }
    }
}