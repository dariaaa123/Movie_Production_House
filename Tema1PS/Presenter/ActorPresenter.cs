
using Tema1PS.Model;
using Tema1PS.Model.Repositories;




namespace Tema1PS.Presenter
{
    public class ActorPresenter:IEmployeeGUI<ActorDTO>
    {
        private readonly ActorRepository _actorRepository;

        public ActorPresenter(ActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        // Retrieve all actors
        public async Task<List<ActorDTO>> GetEmployeesAsync()
        {
            var actors = await _actorRepository.GetAllAsync();

            return actors.Select(actor => new ActorDTO()
            {
                Id = actor.Id, // Include ID for updates and deletes
                Name = actor.Name
            }).ToList();
        }

        // Insert a new actor
        public async Task AddEmployeeAsync(string name)
        {
            var newActor = new Actor { Name = name };
            await _actorRepository.InsertAsync(newActor);
        }

        // Update an existing actor
        public async Task UpdateEmployeeAsync(int id, string newName)
        {
            var actor = await _actorRepository.GetByIdAsync(id);
            if (actor != null)
            {
                actor.Name = newName;
                await _actorRepository.UpdateAsync(actor);
            }
        }

        // Delete an actor by ID
        public async Task DeleteEmployeeAsync(int id)
        {
            await _actorRepository.DeleteAsync(id);
        }
        
        public async Task<ActorDTO> GetEmployeeByIdAsync(int id)
        {
            // Fetch the actor from the repository by ID
            var actor = await _actorRepository.GetByIdAsync(id);

            if (actor == null)
            {
                return null; // Return null if no actor is found
            }

            // Return the actor as an ActorDTO
            return new ActorDTO
            {
                Id = actor.Id,
                Name = actor.Name
            };
        }

      
    }
}