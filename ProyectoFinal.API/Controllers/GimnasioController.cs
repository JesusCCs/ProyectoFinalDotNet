using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.API.Controllers
{
    [Route("/gimnasio")]
    [ApiController]
    public class GimnasioController : ControllerBase
    {
        private readonly IGinmasioBL _bl;
        
        public GimnasioController(IGinmasioBL bl)
        {
            _bl = bl;
        }
        
        [HttpPost]
        [Route("/gimnasio/login")]
        public async Task<ActionResult> Login([FromBody] GimnasioLoginDto itemLogin)
        {
            var item = await _bl.Login(itemLogin);
            return item == null ? Ok(false) : Ok(item);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var lista = await _bl.GetAll();
            return Ok(lista);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var item = await _bl.GetById(id);
            return Ok(item);
        }
        
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] GimnasioCreateDto itemNuevo)
        {
            var item = await _bl.Create(itemNuevo);
            return Ok(item);
        }
        

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] GimnasioUpdateDto itemActualizado)
        {
            var item = await _bl.Update(id, itemActualizado);
            return Ok(item);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var item = await _bl.Delete(id);
            return Ok(item);
        }
    }
}
