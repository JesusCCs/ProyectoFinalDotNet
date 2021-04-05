using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        public LoginController()
        {
            
        }

        [HttpGet]
        public string Get()
        {
            return "Hola";
        }
    }
}