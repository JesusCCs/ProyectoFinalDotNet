using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = Rol.Admin)]
    public class RolController : ControllerBase
    {
        private readonly RoleManager<Rol> _roleManager;

        public RolController(RoleManager<Rol> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole([Required] string roleName)
        {
            var roleResult = await _roleManager.CreateAsync(new Rol {Name = roleName});

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }
    }
}