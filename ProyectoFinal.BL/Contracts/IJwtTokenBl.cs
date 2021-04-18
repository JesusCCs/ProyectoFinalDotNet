using System;

namespace ProyectoFinal.BL.Contracts
{
    public interface IJwtTokenBl
    {
        string GenerateJwtToken(Guid authId, string rol);
    }
}