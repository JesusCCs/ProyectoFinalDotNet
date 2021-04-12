using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;

namespace ProyectoFinal.API.Controllers
{
    /// <inheritdoc />
    [Route("/gimnasio")]
    [ApiController]
    public class GimnasioController : ControllerBase
    {
        private readonly IGinmasioBl _bl;

        /// <inheritdoc />
        public GimnasioController(IGinmasioBl bl)
        {
            _bl = bl;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemLogin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/gimnasio/login")]
        public async Task<ActionResult> Login([FromBody] GimnasioLoginDto itemLogin)
        {
            var item = await _bl.Login(itemLogin);
            return item == null ? Ok(false) : Ok(item);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var lista = await _bl.GetAll();
            return Ok(lista);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var item = await _bl.GetById(id);
            return Ok(item);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] GimnasioCreateDto itemNuevo)
        {
            var item = await _bl.Create(itemNuevo);
            return Ok(item);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemActualizado"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] GimnasioUpdateDto itemActualizado)
        {
            var item = await _bl.Update(id, itemActualizado);
            return Ok(item);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var item = await _bl.Delete(id);
            return Ok(item);
        }
    }
}
