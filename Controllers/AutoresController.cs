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
    [Route("api/[controller]")]
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

        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> GetFirst()
        {
            return await _dbContext.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> GetId(int id)
        {
            var autor = await _dbContext.Autores.FirstOrDefaultAsync(x => x.id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> GetNombre(string nombre)
        {
            var autor = await _dbContext.Autores.FirstOrDefaultAsync(x => x.nombre.Contains(nombre));

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
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