using Microsoft.EntityFrameworkCore;
using Tema1PS.DataBase;

namespace Tema1PS.Model.RepositoryPack;

public class MovieRepository
{
    private readonly ProdHouseContext _context;
    private readonly DbSet<Movie> _dbSet;

    public MovieRepository(ProdHouseContext context)
    {
        _context = context;
        _dbSet = context.Set<Movie>();
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Movie> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<int> InsertAsync(Movie movie)
    {
        _dbSet.Add(movie);
        await _context.SaveChangesAsync();
        
        return movie.Id; // Return the generated ID
    }

    public async Task UpdateAsync(Movie movie)
    {
        _dbSet.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var movie = await _dbSet.FindAsync(id);
        if (movie != null)
        {
            _dbSet.Remove(movie);
            await _context.SaveChangesAsync();
        }
    }
}