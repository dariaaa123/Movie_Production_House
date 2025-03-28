using Microsoft.EntityFrameworkCore;
using Tema1PS.DataBase;

namespace Tema1PS.Model.RepositoryPack;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ProdHouseContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ProdHouseContext context)
    {
        _context = context;
        _dbSet = context.Set<T>(); 
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task InsertAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}