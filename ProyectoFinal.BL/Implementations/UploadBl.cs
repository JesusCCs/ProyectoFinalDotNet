using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core;

namespace ProyectoFinal.BL.Implementations
{
    public class UploadBl : IUploadBl
    {
        private readonly IWebHostEnvironment _env;

        public UploadBl(IWebHostEnvironment env)
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

        private string CreateDestiny(FileType type)
        {
            return type switch
            {
                FileType.Logo => Path.Combine(_env.WebRootPath,"logos"),
                FileType.Anuncio => Path.Combine(_env.WebRootPath,"anuncios"),
                _ => null
            };
        }
    }
}