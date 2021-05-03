using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProyectoFinal.BL.Contracts
{
    public interface IUploadBl
    {
        Task Upload(IFormFile file);
    }
}