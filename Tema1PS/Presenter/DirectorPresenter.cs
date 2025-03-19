using Tema1PS.Model;
using Tema1PS.Model.Repositories;

namespace Tema1PS.Presenter;

public class DirectorPresenter:IEmployeeGUI<DirectorDTO>
{
    private readonly DirectorRepository _directorRepository;

    public DirectorPresenter(DirectorRepository directorRepository)
    {
        _directorRepository = directorRepository;
    }

    // Retrieve all actors
    public async Task<List<DirectorDTO>> GetEmployeesAsync()
    {
        var directors = await _directorRepository.GetAllAsync();

        return directors.Select(director => new DirectorDTO()
        {
            Id = director.Id, // Include ID for updates and deletes
            Name = director.Name
        }).ToList();
    }

    // Insert a new actor
    public async Task AddEmployeeAsync(string name)
    {
        var newDirector = new Director { Name = name };
        await _directorRepository.InsertAsync(newDirector);
    }

    // Update an existing actor
    public async Task UpdateEmployeeAsync(int id, string newName)
    {
        var actor = await _directorRepository.GetByIdAsync(id);
        if (actor != null)
        {
            actor.Name = newName;
            await _directorRepository.UpdateAsync(actor);
        }
    }

    // Delete an actor by ID
    public async Task DeleteEmployeeAsync(int id)
    {
        await _directorRepository.DeleteAsync(id);
    }
    
    public async Task<DirectorDTO> GetEmployeeByIdAsync(int id)
    {
        // Fetch the actor from the repository by ID
        var director = await _directorRepository.GetByIdAsync(id);

        if (director == null)
        {
            return null; // Return null if no actor is found
        }

        // Return the actor as an ActorDTO
        return new DirectorDTO
        {
            Id = director.Id,
            Name = director.Name
        };
    }
}