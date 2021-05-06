﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.Core;

namespace ProyectoFinal.BL.Helpers
{
    public class FileManager
    {
        private readonly IWebHostEnvironment _env;

        public FileManager(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> Upload(IFormFile file, FileType type)
        {
            var mime = Path.GetExtension(file.FileName);
            var fileName = string.Concat(Path.GetRandomFileName(), mime);

            var savePath = Path.Combine(CreateDestiny(type), fileName);

            await using var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
            await file.CopyToAsync(fileStream);

            return fileName;
        }
        
        public void Remove(string fileName, FileType type)
        {
            var path = Path.Combine(CreateDestiny(type), fileName);
            File.Delete(path);
        }

        private string CreateDestiny(FileType type)
        {
            return type switch
            {
                FileType.Logo => Path.Combine(_env.WebRootPath, "logos"),
                FileType.Anuncio => Path.Combine(_env.WebRootPath, "anuncios"),
                _ => null
            };
        }
    }
}