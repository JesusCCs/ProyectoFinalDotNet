using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IGinmasioBl
    {
        Task<GimnasioGetByIdResponseDto> Create(GimnasioCreateDto gimnasio, Guid authId);
        Task<IEnumerable<GimnasioGetAllResponseDto>> GetAll();
        Task<GimnasioGetByIdResponseDto> GetById(Guid id);
        Task<GimnasioGetByIdResponseDto> GetByAuthId(Guid guidAuth);
        Task<Guid> GetIdByAuthId(Guid guidAuth);
        Task<bool> Update(Guid id, GimnasioUpdateDto gimnasio);
        Task<bool> Delete(Guid id);
    }
}