using ApiCatalogo.Context;
using ApiCatalogo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase { 
    
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> Get()
    {
        var produtos = await _context.Produtos.Take(10).ToListAsync();
        if(produtos is null)
        {
            return NotFound("Produtos não encontrados");
        }
        return produtos;
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]

    public async Task<ActionResult<Produto>> Get(int id)
    {
  

        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        return produto;
    }

    [HttpPost]
    public ActionResult Post([FromBody]Produto  produto)
    {    
       

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto", new {id = produto.ProdutoId}, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if(id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if(produto is null)
        {
            return NotFound("Produto não localizado...");
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }
}
