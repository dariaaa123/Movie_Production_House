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

        public void SetEmployeeGUI(IEmployeeGUI employeeGUI)
        {
            _employeeGUI = employeeGUI ?? throw new ArgumentNullException(nameof(employeeGUI));
        }
        
        public async Task<List<ActorDTO>> GetActorsAsync()
        {
            var actors = await _actorRepository.GetAllAsync();

            return actors.Select(actor => new ActorDTO()
            {
                Id = actor.Id,
                Name = actor.Name
            }).ToList();
        }
        
        public async Task AddActorAsync()
        {
            var name = _employeeGUI.GetNewEmployeeName(); 
            var newActor = new Actor { Name = name };
            await _actorRepository.InsertAsync(newActor);
        }


        public async Task UpdateActorAsync()
        {
            int id = _employeeGUI.GetEmployeeId();  
            var newName = _employeeGUI.GetEmployeeName();  

            var actor = await _actorRepository.GetByIdAsync(id);
            if (actor != null)
            {
                actor.Name = newName;
                await _actorRepository.UpdateAsync(actor);
            }
        }

    
        public async Task DeleteActorAsync()
        {
            int id = _employeeGUI.GetDeletingEmployeeId(); 
            await _actorRepository.DeleteAsync(id);
        }
        
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
