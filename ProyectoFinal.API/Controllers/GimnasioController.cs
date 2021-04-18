using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/gimnasio")]
    public class GimnasioController : ControllerBase
    {
        private readonly IGinmasioBl _gimnasioBl;
        private readonly IAuthBl _authBl;

        public GimnasioController(IGinmasioBl gimnasioBl, IAuthBl authBl)
        {
            _gimnasioBl = gimnasioBl;
            _authBl = authBl;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/gimnasio/login")]
        public async Task<ActionResult> Login([FromBody] AuthSignInDto itemLogin)
        {
            var guidAuth = await _authBl.SignIn(itemLogin);

            if (guidAuth == null)
            {
                return Ok(false);
            }

            var gimansio = await _gimnasioBl.GetByAuthId(guidAuth);
            
            return Ok(gimansio);
        }
        
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var lista = await _gimnasioBl.GetAll();
            return Ok(lista);
        }
        
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var item = await _gimnasioBl.GetById(id);
            return Ok(item);
        }
        
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromBody] GimnasioCreateDto itemNuevo)
        {
            var guid = await _authBl.SignUp(itemNuevo, Rol.Gimnasio);

            if (guid == null)
            {
                return Problem("Fallo al generar el usuario", null, 500);
            }

            var item = await _gimnasioBl.Create(itemNuevo, (Guid) guid);

            return Ok(item);
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] GimnasioUpdateDto itemActualizado)
        {
            var item = await _gimnasioBl.Update(id, itemActualizado);
            return Ok(item);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var item = await _gimnasioBl.Delete(id);
            return Ok(item);
        }
    }
}