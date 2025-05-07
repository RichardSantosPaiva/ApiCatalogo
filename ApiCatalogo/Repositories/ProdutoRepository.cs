﻿using ApiCatalogo.Context;
using ApiCatalogo.Model;

namespace ApiCatalogo.Repositories;

    public class ProdutoRepository :  Repository<Produto>,IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context):base(context)
        { }

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(p => p.CategoriaId == id);    
    }
}
