using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.Core.Exceptions;

namespace ProyectoFinal.Core.Helpers
{
    public static class FormFileExtension
    {
        public static string GetTipo(this IFormFile file)
        {
            var mime = Path.GetExtension(file.FileName);

            switch (mime)
            {
                case ".jpg":
                case ".png":
                case ".jpeg": return AnunciosTipo.Imagen;
                case ".gif": return AnunciosTipo.Gif;
                case ".mp4":
                case ".avi":
                case ".webm": return AnunciosTipo.Video;
            }

            throw new AppException(new Dictionary<string, string[]>
            {
                ["Recurso"] = new []{ "Formato de archivo inválido" }
            });
        }
    }
}