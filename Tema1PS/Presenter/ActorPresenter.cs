using Tema1PS.Model;
using Tema1PS.Model.Repositories;

namespace Tema1PS.Presenter
{
    public class ActorPresenter
    {
        private IEmployeeGUI _employeeGUI;
        private readonly ActorRepository _actorRepository;

       
        public ActorPresenter(ActorRepository actorRepository)
        {
            _actorRepository = actorRepository ?? throw new ArgumentNullException(nameof(actorRepository));
        }

        // Set the GUI (in this case, the Actor's Blazor page)
        public void SetEmployeeGUI(IEmployeeGUI employeeGUI)
        {
            _employeeGUI = employeeGUI ?? throw new ArgumentNullException(nameof(employeeGUI));
        }

        // Retrieve all actors
        public async Task<List<ActorDTO>> GetActorsAsync()
        {
            var actors = await _actorRepository.GetAllAsync();

            return actors.Select(actor => new ActorDTO()
            {
                Id = actor.Id,
                Name = actor.Name
            }).ToList();
        }

        // Insert a new actor
        public async Task AddActorAsync()
        {
            var name = _employeeGUI.GetNewEmployeeName();  // Get the name from the GUI
            var newActor = new Actor { Name = name };
            await _actorRepository.InsertAsync(newActor);
        }

        // Update an existing actor
        public async Task UpdateActorAsync()
        {
            int id = _employeeGUI.GetEmployeeId();  // Get the actor's ID from the GUI
            var newName = _employeeGUI.GetEmployeeName();  // Get the updated name from the GUI

            var actor = await _actorRepository.GetByIdAsync(id);
            if (actor != null)
            {
                actor.Name = newName;
                await _actorRepository.UpdateAsync(actor);
            }
        }

        // Delete an actor by ID
        public async Task DeleteActorAsync()
        {
            int id = _employeeGUI.GetDeletingEmployeeId();  // Get the ID from the GUI
            await _actorRepository.DeleteAsync(id);
            //await LoadActors();
        }

        // Get actor details by ID
        public async Task<ActorDTO> GetActorByIdAsync()
        {
            int id = _employeeGUI.GetEmployeeId();
            var actor = await _actorRepository.GetByIdAsync(id);
            if (actor == null) return null;

            return new ActorDTO
            {
                Id = actor.Id,
                Name = actor.Name
            };
        }
    }
}
