﻿using System.Linq.Expressions;
using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        Console.WriteLine("Repository constructor called");
        _context = context;
        if (_context == null)
        {
            Console.WriteLine("AppDbContext is NULL!");
        }
    }

    public IEnumerable<T> GetAll()
    {
       return _context.Set<T>().AsNoTracking().ToList();
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
       // _context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        //_context.Entry(entity).State = EntityState.Modified;
       // _context.SaveChanges();
        return entity;
    }

   

    public T Delete(T entity)
    {
       _context.Set<T>().Remove(entity);
      //  _context.SaveChanges();
        return entity;
    }   
}
