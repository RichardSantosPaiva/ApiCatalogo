using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Model;
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
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public CategoriasController(AppDbContext context, ILogger<CategoriasController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            // return _context.Categorias.Include(p => p.Produtos).ToList();
            _logger.LogInformation("========= GET api/categorias/produtos ===========");  
            return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();

        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilters))]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            _logger.LogInformation("========= GET api/categorias ===========");

            try
            {
                return await _context.Categorias.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }



        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria == null)
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

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                _logger.LogWarning($"Dados inválidos...");
                return BadRequest("Dados Inválidos");    
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound($"Categoria com id={id} não encontrada...");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }

    }
}
