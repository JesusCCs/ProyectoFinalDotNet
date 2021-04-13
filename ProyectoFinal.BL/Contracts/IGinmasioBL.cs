using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IGinmasioBl
    {
        Task<GimnasioDetallesDto> Login(GimnasioLoginDto login);
        Task<GimnasioDetallesDto> Create(GimnasioCreateDto gimnasio);
        Task<IEnumerable<GimnasioListaDto>> GetAll();
        Task<GimnasioDetallesDto> GetById(Guid id);
        Task<bool> Update(Guid id, GimnasioUpdateDto gimnasio);
        Task<bool> Delete(Guid id);
    }
}