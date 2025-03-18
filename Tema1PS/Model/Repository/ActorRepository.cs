// Model/Repositories/ActorRepository.cs

using Microsoft.EntityFrameworkCore;
using Tema1PS.DataBase;

namespace Tema1PS.Model.Repositories
{
    public class ActorRepository
    {
        private readonly ProdHouseContext _context;

        public ActorRepository(ProdHouseContext context)
        {
            _context = context;
        }

        // Fetch all actors
        public async Task<List<Actor>> GetAllActorsAsync()
        {
            return await _context.Actors.ToListAsync();
        }

        // Fetch a specific actor by ID
        public async Task<Actor> GetActorByIdAsync(int id)
        {
            return await _context.Actors.FindAsync(id);
        }

        public async Task InsertActorAsync(Actor actor)
        {
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            _context.Actors.Add(actor);
            Console.WriteLine("BBBBBBBBBBBBBBBBBBBBBBBBBBBB");
            await _context.SaveChangesAsync();
            Console.WriteLine("CCCCCCCCCCCCCCCCCCCCCCCCCCCCC");
        }

        public async Task UpdateActorAsync(Actor actor)
        {
            _context.Actors.Update(actor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteActorAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
            }
        }
    }
}