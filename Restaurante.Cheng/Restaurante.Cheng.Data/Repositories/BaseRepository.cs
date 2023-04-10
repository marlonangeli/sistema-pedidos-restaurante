using Microsoft.EntityFrameworkCore;
using Restaurante.Cheng.Data.Context;
using Restaurante.Cheng.Domain.Interfaces;

namespace Restaurante.Cheng.Data.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly RestauranteDbContext _context;
    
    public BaseRepository(RestauranteDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

    public IQueryable<T> GetQueryable() => _context.Set<T>().AsQueryable();

    public async Task<T?> GetByIdAsync(object? id) => await _context.Set<T>().FindAsync(id);

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(object id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        _context.Set<T>().Remove(entity ?? throw new InvalidOperationException("Entity not found"));
        await _context.SaveChangesAsync();
        return entity;
    }
}