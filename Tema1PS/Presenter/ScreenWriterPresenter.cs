using Tema1PS.Model;
using Tema1PS.Model.RepositoryPack;

namespace Tema1PS.Presenter;

public class ScreenWriterPresenter:IEmployeeGUI<ScreenWriterDTO>
{
    private readonly ScreenWriterRepository _screenWriterRepository;

    public ScreenWriterPresenter(ScreenWriterRepository screenWriterRepository)
    {
        _screenWriterRepository = screenWriterRepository;
    }

    // Retrieve all screenwriters
    public async Task<List<ScreenWriterDTO>> GetEmployeesAsync()
    {
        var screenWriters = await _screenWriterRepository.GetAllAsync();

        return screenWriters.Select(screenWriter => new ScreenWriterDTO()
        {
            Id = screenWriter.Id, // Include ID for updates and deletes
            Name = screenWriter.Name
        }).ToList();
    }

    // Insert a new screenwriter
    public async Task AddEmployeeAsync(string name)
    {
        var newScreenWriter = new ScreenWriter { Name = name };
        await _screenWriterRepository.InsertAsync(newScreenWriter);
    }

    // Update an existing screenwriter
    public async Task UpdateEmployeeAsync(int id, string newName)
    {
        var screenWriter = await _screenWriterRepository.GetByIdAsync(id);
        if (screenWriter != null)
        {
            screenWriter.Name = newName;
            await _screenWriterRepository.UpdateAsync(screenWriter);
        }
    }

    // Delete a screenwriter by ID
    public async Task DeleteEmployeeAsync(int id)
    {
        await _screenWriterRepository.DeleteAsync(id);
    }
    
    public async Task<ScreenWriterDTO> GetEmployeeByIdAsync(int id)
    {
        // Fetch the actor from the repository by ID
        var screenwriter = await _screenWriterRepository.GetByIdAsync(id);

        if (screenwriter == null)
        {
            return null; // Return null if no actor is found
        }

        // Return the actor as an ActorDTO
        return new ScreenWriterDTO()
        {
            Id = screenwriter.Id,
            Name = screenwriter.Name
        };
    }
}