using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.API.Controllers
{
    [ApiController]
    [Route("rol")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rol.Admin)]
    public class RolController : ControllerBase
    {
        private readonly RoleManager<Rol> _roleManager;
        private readonly IWebHostEnvironment _env;

        public RolController(RoleManager<Rol> roleManager, IWebHostEnvironment env)
        {
            _roleManager = roleManager;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([Required] string roleName)
        {
            if (!_env.IsDevelopment())
            {
                return NotFound();
            }
            
            var roleResult = await _roleManager.CreateAsync(new Rol {Name = roleName});

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }
    }
}