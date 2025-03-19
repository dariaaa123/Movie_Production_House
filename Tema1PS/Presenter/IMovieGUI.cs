namespace Tema1PS.Presenter;
using Tema1PS.Model;

public interface IMovieGUI
{
    // Retrieve all movies, including related data like Director, ScreenWriter, and Actors
    Task<List<MovieDTO>> GetMoviesAsync();

    // Retrieve a specific movie by ID, including its related data
    Task<MovieDTO> GetMovieByIdAsync(int id);

    // Add a new movie with details (title, year, director, screenwriter, and actors)
    Task AddMovieAsync(string title, int year, int directorId, int screenWriterId, List<int> actorIds);

    // Update an existing movie, including changes to title, year, director, screenwriter, and actor list
    Task UpdateMovieAsync(int id, string newTitle, int newYear, int newDirectorId, int newScreenWriterId, List<int> newActorIds);

    // Delete a movie by ID, ensuring associated actor relationships are also removed
    Task DeleteMovieAsync(int id);
}
