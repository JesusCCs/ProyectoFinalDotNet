using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinal.API.Authorization.Requirements
{
    public class GymIsOwnerRequirement : IAuthorizationRequirement
    {
        public bool SameId(string ownerId,string targetId)
        {
            return ownerId == targetId;
        }
    }
}