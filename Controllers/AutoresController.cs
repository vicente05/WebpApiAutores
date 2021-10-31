using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebpApiAutores.Models;
using DevExtreme.AspNet.Data;
using System.Linq;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using WebpApiAutores.Filtros;
using System;

namespace WebpApiAutores.Controllers
{
    [ApiController]
    [Route("autores")]
    public class AutoresController : ControllerBase
    {

        private readonly ApplicationDbcontext _dbContext;

        public AutoresController(ApplicationDbcontext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await _dbContext.Autores.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> GetId(int id)
        {
            var autor = await _dbContext.Autores.FirstOrDefaultAsync(x => x.id == id);

            if (autor == null)
            {
                return NotFound("no se ha podido encontrar el autor");
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
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {

            var existeAutorConElMismoNombre = await _dbContext.Autores.AnyAsync(x => x.nombre == autor.nombre);

            if (existeAutorConElMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre { autor.nombre }");
            }

            _dbContext.Add(autor);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Autor autor)
        {
            var existe = await _dbContext.Autores.AnyAsync(x => x.id == autor.id);

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

        #region otrasConsultas

        //[HttpGet]
        //[ResponseCache(Duration = 10)]
        //public async Task<ActionResult<LoadResult>> Lista([FromQuery] DataSourceLoadOptions loadOptions)
        //{
        //    var autores = _dbContext.Autores.Select(x => new { x.nombre, x.id, prueba = "lalalala" });
        //    loadOptions.PrimaryKey = new[] { "id" };
        //    loadOptions.PaginateViaPrimaryKey = true;
        //    return await DataSourceLoader.LoadAsync(autores, loadOptions);
        //}

        //[HttpDelete]
        //public async Task<ActionResult> DeleteIds([FromQuery] int[] ids)
        //{
        //    using var transaction = await _dbContext.Database.BeginTransactionAsync();
        //    try
        //    {
        //        var existe = await _dbContext.Autores.Where(x => ids.Contains(x.id)).ToListAsync();
        //        if (existe.Count == 0)
        //        {
        //            return NotFound();
        //        }
        //        _dbContext.Autores.RemoveRange(existe);
        //        await _dbContext.SaveChangesAsync();
        //        await transaction.CommitAsync();
        //        return Ok(ids);
        //    }
        //    catch (System.Exception error)
        //    {
        //        await transaction.RollbackAsync();
        //        return BadRequest(error.Message);
        //    }
        //}

        #endregion otrasConsultas


    }
}