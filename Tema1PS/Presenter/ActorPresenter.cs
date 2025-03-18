// Presenter/Services/ActorService.cs
using Tema1PS.Model;
using Tema1PS.Model.Repositories;
using Tema1PS.Presenter;

//using YourProject.ViewModels;

namespace Tema1PS.Presenter
{
    public class ActorPresenter:IActorGUI
    {
        private readonly ActorRepository _actorRepository;

        public ActorPresenter(ActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        // Retrieve all actors
        public async Task<List<ActorDTO>> GetActorsAsync()
        {
            var actors = await _actorRepository.GetAllActorsAsync();

            return actors.Select(actor => new ActorDTO()
            {
                Id = actor.Id, // Include ID for updates and deletes
                Name = actor.Name
            }).ToList();
        }

        // Insert a new actor
        public async Task AddActorAsync(string name)
        {
            var newActor = new Actor { Name = name };
            await _actorRepository.InsertActorAsync(newActor);
        }

        // Update an existing actor
        public async Task UpdateActorAsync(int id, string newName)
        {
            var actor = await _actorRepository.GetActorByIdAsync(id);
            if (actor != null)
            {
                actor.Name = newName;
                await _actorRepository.UpdateActorAsync(actor);
            }
        }

        // Delete an actor by ID
        public async Task DeleteActorAsync(int id)
        {
            await _actorRepository.DeleteActorAsync(id);
        }
    }
}