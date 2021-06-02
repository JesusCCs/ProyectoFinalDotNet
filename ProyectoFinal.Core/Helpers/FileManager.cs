using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.Core.Exceptions;

namespace ProyectoFinal.Core.Helpers
{
    public class FileManager
    {
        private readonly IWebHostEnvironment _env;
        private readonly Server _server;
        private readonly Mobile _mobile;

        public FileManager(IWebHostEnvironment env, Server server, Mobile mobile)
        {
            _env = env;
            _server = server;
            _mobile = mobile;
        }

        public async Task<string> Upload(IFormFile file, FileType type)
        {
            var mime = Path.GetExtension(file.FileName);
            var fileName = string.Concat(Path.GetRandomFileName(), mime);

            var savePath = Path.Combine(CreateDestiny(type), fileName);

            await using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream);
            }

            if (type != FileType.Anuncio || file.GetTipo() != AnunciosTipo.Video) return fileName;
            
            var ruta = _env.ContentRootPath + "\\..\\ProyectoFinal.Core\\Helpers\\FFProbe";
            var ffProbe = new NReco.VideoInfo.FFProbe {ToolPath = ruta};
            var videoInfo = ffProbe.GetMediaInfo(savePath);

            if (videoInfo.Duration < TimeSpan.FromSeconds(21)) return fileName;

            Remove(savePath, FileType.Anuncio);
            throw new TimeVideoExceeded();
        }

        public void Remove(string fileName, FileType type)
        {
            var path = Path.Combine(CreateDestiny(type), fileName);
            File.Delete(path);
        }

        public string Get(string? fileName, FileType type)
        {
            return (fileName is null or "")
                ? CreateOrigin(FileType.Logo, false) + "/" + "default.jpg"
                : CreateOrigin(type, false) + "/" + fileName;
        }
        
        public string Get(string? fileName, FileType type, bool isMobile)
        {
            return fileName is null or ""
                ? CreateOrigin(FileType.Logo, isMobile) + "/" + "default.jpg"
                : CreateOrigin(type, isMobile) + "/" + fileName;
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

        private string CreateOrigin(FileType type, bool isMobile)
        {
            var path = isMobile ? _mobile.Url : _server.Url;
            return type switch
            {
                FileType.Logo => path +  "/logos",
                FileType.Anuncio => path + "/anuncios",
                _ => null
            };
        }
    }
}