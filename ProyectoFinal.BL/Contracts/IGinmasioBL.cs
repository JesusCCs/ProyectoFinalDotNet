using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;

namespace ProyectoFinal.BL.Contracts
{
    public interface IGinmasioBl
    {
        Task<Gimnasio> Create(GimnasioCreateRequest gimnasio, Guid authId);
        Task<IEnumerable<GimnasioGetAllResponse>> GetAll();
        Task<GimnasioGetByIdResponse> GetById(Guid id);
        Task<GimnasioGetByIdResponse> GetByAuthId(Guid guidAuth);
        Task<Guid> GetIdByAuthId(Guid guidAuth);
        Task<bool> Update(Guid id, GimnasioUpdateRequest gimnasio);
        Task<bool> Delete(Guid id);
    }
}