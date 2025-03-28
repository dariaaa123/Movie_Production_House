using Tema1PS.Model;
using Tema1PS.Model.Repositories;

namespace Tema1PS.Presenter
{
    public class DirectorPresenter
    {
        private IEmployeeGUI _employeeGUI;
        private readonly DirectorRepository _directorRepository;

        public DirectorPresenter(DirectorRepository directorRepository)
        {
            _directorRepository = directorRepository ?? throw new ArgumentNullException(nameof(directorRepository));
        }
        
        public void SetEmployeeGUI(IEmployeeGUI employeeGUI)
        {
            _employeeGUI = employeeGUI ?? throw new ArgumentNullException(nameof(employeeGUI));
        }
        
        public async Task<List<DirectorDTO>> GetDirectorsAsync()
        {
            var directors = await _directorRepository.GetAllAsync();

            return directors.Select(director => new DirectorDTO()
            {
                Id = director.Id,
                Name = director.Name
            }).ToList();
        }
        
        public async Task AddDirectorAsync()
        {
            var name = _employeeGUI.GetNewEmployeeName();  
            var newDirector = new Director { Name = name };
            await _directorRepository.InsertAsync(newDirector);
        }

        public async Task UpdateDirectorAsync()
        {
            int id = _employeeGUI.GetEmployeeId();  
            var newName = _employeeGUI.GetEmployeeName(); 

            var director = await _directorRepository.GetByIdAsync(id);
            if (director != null)
            {
                director.Name = newName;
                await _directorRepository.UpdateAsync(director);
            }
        }

   
        public async Task DeleteDirectorAsync()
        {
            int id = _employeeGUI.GetDeletingEmployeeId();  
            await _directorRepository.DeleteAsync(id);
        }

        public async Task<DirectorDTO> GetDirectorByIdAsync()
        {
            int id = _employeeGUI.GetEmployeeId();
            var director = await _directorRepository.GetByIdAsync(id);
            if (director == null) return null;

            return new DirectorDTO
            {
                Id = director.Id,
                Name = director.Name
            };
        }
    }
}
