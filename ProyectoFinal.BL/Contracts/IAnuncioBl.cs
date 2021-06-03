#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;

namespace ProyectoFinal.BL.Contracts
{
    public interface IAnuncioBl
    {
        Task<Guid> Create(AnuncioCreateRequest request);
        Task<bool> CheckDates(DateTime inicio, DateTime fin);
        Task<AnuncioDetallesResponse> GetById(Guid id);
        Task<IEnumerable<AnuncioBaseResponse>> GetAllFrom(Guid id);
        Task<AnuncioBaseResponse> UpdateDetails(Guid id, AnuncioUpdateDetailsRequest request);
        Task<Guid> UpdateRecurso(Guid id, AnuncioUpdateRecursoRequest request);
        Task<AnuncioBaseResponse> ConfirmCreation(Guid id, bool finalizado);
        Task<AnuncioToWatchResponse?> GetAdToWatch();
        Task MarkAdAsWatched(Guid guid, AnuncioWatchedRequest request);
    }
}