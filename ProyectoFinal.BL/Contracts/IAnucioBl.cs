using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;

namespace ProyectoFinal.BL.Contracts
{
    public interface IAnucioBl
    {
        Task<Anuncio> Create(AnuncioCreateRequest request);
        Task<IEnumerable<AnuncioCheckAllResponse>> CheckDates();
        Task<AnuncioGetByIdResponse> GetById(Guid id);
    }
}