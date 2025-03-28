using Tema1PS.Model;
using Tema1PS.Model.Repositories;
using Tema1PS.Model.RepositoryPack;

namespace Tema1PS.Presenter
{
    public class ScreenWriterPresenter
    {
        private IEmployeeGUI _employeeGUI;
        private readonly ScreenWriterRepository _screenWriterRepository;

        public ScreenWriterPresenter(ScreenWriterRepository screenWriterRepository)
        {
            _screenWriterRepository = screenWriterRepository ?? throw new ArgumentNullException(nameof(screenWriterRepository));
        }
        
        public void SetEmployeeGUI(IEmployeeGUI employeeGUI)
        {
            _employeeGUI = employeeGUI ?? throw new ArgumentNullException(nameof(employeeGUI));
        }
        
        public async Task<List<ScreenWriterDTO>> GetScreenWritersAsync()
        {
            var screenwriters = await _screenWriterRepository.GetAllAsync();

            return screenwriters.Select(screenWriter => new ScreenWriterDTO()
            {
                Id = screenWriter.Id,
                Name = screenWriter.Name
            }).ToList();
        }
        
        public async Task AddScreenWriterAsync()
        {
            var name = _employeeGUI.GetNewEmployeeName();  
            var newScreenWriter = new ScreenWriter { Name = name };
            await _screenWriterRepository.InsertAsync(newScreenWriter);
        }

  
        public async Task UpdateScreenWriterAsync()
        {
            int id = _employeeGUI.GetEmployeeId();
            var newName = _employeeGUI.GetEmployeeName();  

            var screenWriter = await _screenWriterRepository.GetByIdAsync(id);
            /*if (screenWriter != null)
            {
                screenWriter.Name = newName;
                await _screenWriterRepository.UpdateAsync(screenWriter);
            }*/
        }
        
        public async Task DeleteScreenWriterAsync()
        {
            int id = _employeeGUI.GetDeletingEmployeeId();  
            await _screenWriterRepository.DeleteAsync(id);
          
        }
        
        public async Task<ScreenWriterDTO> GetScreenWriterByIdAsync()
        {
            int id = _employeeGUI.GetEmployeeId();
            var screenWriter = await _screenWriterRepository.GetByIdAsync(id);
            if (screenWriter == null) return null;

            return new ScreenWriterDTO
            {
                Id = screenWriter.Id,
                Name = screenWriter.Name
            };
        }
    }
}
