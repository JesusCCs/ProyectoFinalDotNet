using System;

namespace ProyectoFinal.BL.Contracts
{
    public interface IJwtTokenBl
    {
        string GenerateJwtToken(Guid entityId, Guid authId, string rol);
    }
}