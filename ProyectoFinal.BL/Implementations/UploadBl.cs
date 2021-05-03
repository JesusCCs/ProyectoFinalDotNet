using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.BL.Contracts;

namespace ProyectoFinal.BL.Implementations
{
    public class UploadBl : IUploadBl
    {
        private readonly IWebHostEnvironment _env;

        public UploadBl(IWebHostEnvironment env)
        {
            _env = env;
        }


        public async Task Upload(IFormFile file)
        {
            var mime = Path.GetExtension(file.FileName);
            var fileName = string.Concat(Path.GetRandomFileName(), mime);
            
            var savePath = Path.Combine(_env.WebRootPath, fileName);

            await using var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite);
            await file.CopyToAsync(fileStream);
        }
    }
}