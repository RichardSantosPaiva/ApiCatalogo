using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Model;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;

        public CategoriasController( IUnitOfWork uof,
            ILogger<CategoriasController> logger)
        {
            _logger = logger;
            _uof = uof;
        }


        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilters))]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            _logger.LogInformation("========= GET api/categorias ===========");
        
            var categorias = _uof.CategoriaRepository.GetAll();
            return Ok(categorias);

        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id) ;

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound($"Categoria com id={id} não encontrada...");

            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                _logger.LogWarning($"Dados inválidos...");
                return BadRequest("Dados inválidos");
            }

            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                _logger.LogWarning($"Dados inválidos...");
                return BadRequest("Dados Inválidos");    
            }

            _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound($"Categoria com id={id} não encontrada...");
            }

            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            return Ok(categoria);
        }

    }
}

