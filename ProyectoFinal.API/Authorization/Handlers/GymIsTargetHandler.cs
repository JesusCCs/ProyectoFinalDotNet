using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProyectoFinal.API.Authorization.Requirements;

namespace ProyectoFinal.API.Authorization.Handlers
{
    public class GymIsTargetHandler : AuthorizationHandler<GymIsTargetRequirement>
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GymIsTargetHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GymIsTargetRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                return Task.CompletedTask;
            }

            var ownerId = context.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var routeData = (_httpContextAccessor.HttpContext ?? throw new InvalidOperationException()).GetRouteData();

            var requestId = routeData.Values["id"]?.ToString();
            
            if (requirement.SameId(ownerId,requestId))
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}