using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IUsuarioBl
    {
        Task<UsuarioDetallesDto> Login(UsuarioLoginDto login);
        Task<UsuarioDetallesDto> Create(UsuarioCreateDto gimnasio);
        Task<IEnumerable<UsuarioListaDto>> GetAll();
        Task<UsuarioDetallesDto> GetById(Guid id);
        Task<bool> Update(Guid id, UsuarioUpdateDto dto);
        Task<bool> Delete(Guid id);
        Task<Guid> GetIdByAuthId(Guid authId);
    }
}