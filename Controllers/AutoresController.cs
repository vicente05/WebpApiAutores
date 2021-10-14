using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebpApiAutores.Models;


namespace WebpApiAutores.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {

        private readonly ApplicationDbcontext _dbContext;


        public AutoresController(ApplicationDbcontext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await _dbContext.Autores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            _dbContext.Add(autor);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.id != id)
            {
                return BadRequest("El id del autor no coincide con el de la URL");
            }

            var existe = await _dbContext.Autores.AnyAsync(x => x.id == id);

            if (!existe)
            {
                return NotFound();
            }

            _dbContext.Update(autor);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _dbContext.Autores.AnyAsync(x => x.id == id);

            if (!existe)
            {
                return NotFound();
            }

            _dbContext.Remove(new Autor() { id = id });
            await _dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}