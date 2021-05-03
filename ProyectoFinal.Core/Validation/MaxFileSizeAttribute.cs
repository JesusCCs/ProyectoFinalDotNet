using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ProyectoFinal.Core.Validation
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        
        public MaxFileSizeAttribute(int maxFileSizeInMb)
        {
            _maxFileSize = maxFileSizeInMb;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not IFormFile file) return ValidationResult.Success;
            
            return file.Length > _maxFileSize * 1024 * 1024 ? new ValidationResult(ErrorMessage()) : ValidationResult.Success;
        }

        private new string ErrorMessage()
        {
            return $"El tamaño máximo de archivo permitido es de { _maxFileSize} Mb.";
        }
    }
}