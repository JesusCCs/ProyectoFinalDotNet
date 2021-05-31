using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IUsuarioBl
    {
        Task<Guid> Create(UsuarioCreateRequest usuario, Guid authId);
        Task<Guid> GetIdByAuthId(Guid authId);
    }
}