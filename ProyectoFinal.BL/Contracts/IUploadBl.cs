using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.Core;

namespace ProyectoFinal.BL.Contracts
{
    public interface IUploadBl
    {
        Task<string> Upload(IFormFile file, FileType type);
    }
}