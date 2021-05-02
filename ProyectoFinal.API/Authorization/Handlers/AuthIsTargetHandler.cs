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
    public class AuthIsTargetHandler : AuthorizationHandler<AuthIsTargetRequirement>
    {
                
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthIsTargetHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthIsTargetRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Sid))
            {
                return Task.CompletedTask;
            }

            var originAuthId = context.User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value;

            var routeData = (_httpContextAccessor.HttpContext ?? throw new InvalidOperationException()).GetRouteData();

            var targetAuthId = routeData.Values["AuthId"]?.ToString();
            
            if (requirement.SameId(originAuthId,targetAuthId))
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}