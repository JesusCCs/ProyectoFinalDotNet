using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.API.Authorization.Requirements;
using ProyectoFinal.Core.DTO;
using Request.Body.Peeker;

namespace ProyectoFinal.API.Authorization.Handlers
{
    public class AuthIsTargetHandler : AuthorizationHandler<AuthIsTargetRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthIsTargetHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AuthIsTargetRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Sid))
            {
                return Task.CompletedTask;
            }

            var originAuthId = context.User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value;

            var request = _httpContextAccessor.HttpContext?.Request.PeekBody<ChangePasswordRequest>();

            var targetAuthId = request?.AuthId;

            if (requirement.SameId(originAuthId, targetAuthId.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}