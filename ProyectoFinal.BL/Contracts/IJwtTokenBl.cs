using System;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.BL.Contracts
{
    public interface IJwtTokenBl
    {
        string GenerateAccessToken(Guid entityId, Guid authId, string rol);
        Task<string> GenerateRefreshToken(Guid authId);
        AccessTokenValidatedResponse ValidateAccessToken(string accessToken);
        Task<string> ValidateRefreshToken(string authId,string refreshToken);
    }
}