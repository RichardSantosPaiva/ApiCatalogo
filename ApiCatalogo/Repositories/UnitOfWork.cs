using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository _produtoRepo;

    private ICategoriaRepository _categoriaRepo;

    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context; 
    }

    public IProdutoRepository ProdutoRepository
    {
       
        get
        {
            return _produtoRepo ?? new ProdutoRepository(_context);
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo ?? new CategoriaRepository(_context);
        }
    }


    public void Commit()
    {

    }

    public void Dispose()
    {
        _context.Dispose();
    }   
}
