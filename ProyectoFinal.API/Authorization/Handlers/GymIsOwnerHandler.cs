using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.API.Authorization.Requirements;

namespace ProyectoFinal.API.Authorization.Handlers
{
    public class GymIsOwnerHandler : AuthorizationHandler<GymIsOwnerRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GymIsOwnerHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            GymIsOwnerRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Sid))
            {
                return Task.CompletedTask;
            }

            var routeValues = _httpContextAccessor.HttpContext?.Request.RouteValues;
            
            if (routeValues == null)
            {
                return Task.CompletedTask;
            }

            routeValues.TryGetValue("id", out var targetGymId);

            var originGymId = context.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (requirement.SameId(originGymId, targetGymId?.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}