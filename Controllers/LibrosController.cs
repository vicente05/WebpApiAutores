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
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {

        private readonly ApplicationDbcontext _dbContext;


        public LibrosController(ApplicationDbcontext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await _dbContext.Libros.Include(x => x.autor).FirstOrDefaultAsync(x => x.id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await _dbContext.Autores.AnyAsync(x => x.id == libro.autor_id);
            if (!existeAutor)
            {
                string error = $"No existe el autor de id:{ libro.autor_id }";
                return base.BadRequest(error);
            }

            _dbContext.Add(libro);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }



    }
}