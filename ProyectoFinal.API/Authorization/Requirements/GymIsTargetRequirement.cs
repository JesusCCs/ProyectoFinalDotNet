using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinal.API.Authorization.Requirements
{
    public class GymIsTargetRequirement : IAuthorizationRequirement
    {
        public static bool SameId(string ownerId,string targetId)
        {
            return ownerId == targetId;
        }
    }
}