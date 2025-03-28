using Microsoft.EntityFrameworkCore;
using Tema1PS.DataBase;

namespace Tema1PS.Model.RepositoryPack;

public class MoviesActorsRepository
{
    private readonly ProdHouseContext _context;
    private readonly DbSet<MoviesActors> _dbSet;

    public MoviesActorsRepository(ProdHouseContext context)
    {
        _context = context;
        _dbSet = context.Set<MoviesActors>();
    }

    // Get all actor IDs associated with a movie
    public async Task<List<int>> GetActorsByMovieIdAsync(int movieId)
    {
        return await _dbSet
            .Where(ma => ma.MovieId == movieId)
            .Select(ma => ma.ActorId)
            .ToListAsync();
    }

    // Add a relation between a movie and an actor
    public async Task AddActorToMovieAsync(int movieId, int actorId)
    {
        var relation = new MoviesActors { MovieId = movieId, ActorId = actorId };
        _dbSet.Add(relation);
        await _context.SaveChangesAsync();
    }

    // Remove a specific actor from a movie
    public async Task RemoveActorFromMovieAsync(int movieId, int actorId)
    {
        var relation = await _dbSet
            .FirstOrDefaultAsync(ma => ma.MovieId == movieId && ma.ActorId == actorId);
        if (relation != null)
        {
            _dbSet.Remove(relation);
            await _context.SaveChangesAsync();
        }
    }

    // Remove all actors from a specific movie
    public async Task RemoveAllActorsFromMovieAsync(int movieId)
    {
        var relations = _dbSet.Where(ma => ma.MovieId == movieId);
        _dbSet.RemoveRange(relations);
        await _context.SaveChangesAsync();
    }
}