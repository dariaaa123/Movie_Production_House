using Tema1PS.Model;
using Tema1PS.Model.Repositories;
using Tema1PS.Model.RepositoryPack;

namespace Tema1PS.Presenter;

public class MoviePresenter:IMovieGUI
{
    private readonly MovieRepository _movieRepository;
    private readonly MoviesActorsRepository _moviesActorsRepository;
    private readonly DirectorRepository _directorRepository;
    private readonly ScreenWriterRepository _screenWriterRepository;
    private readonly ActorRepository _actorRepository;

    public MoviePresenter(MovieRepository movieRepository, MoviesActorsRepository moviesActorsRepository,
        DirectorRepository directorRepository,ScreenWriterRepository screenWriterRepository,ActorRepository actorRepository)
    {
        _movieRepository = movieRepository;
        _moviesActorsRepository = moviesActorsRepository;
        _directorRepository = directorRepository;
        _screenWriterRepository = screenWriterRepository;
        _moviesActorsRepository = moviesActorsRepository;
        _actorRepository = actorRepository;
    }

    // Retrieve all movies with related actors
    public async Task<List<MovieDTO>> GetMoviesAsync()
    {
        var movies = await _movieRepository.GetAllAsync();
        var movieDtos = new List<MovieDTO>();

        foreach (var movie in movies)
        {
            var director = await _directorRepository.GetByIdAsync(movie.DirectorId);
            var screenWriter = await _screenWriterRepository.GetByIdAsync(movie.ScreenWriterId);
        
            var actorIds = await _moviesActorsRepository.GetActorsByMovieIdAsync(movie.Id);
            var actorNames = new List<string>();

            foreach (var actorId in actorIds)
            {
                var actor = await _actorRepository.GetByIdAsync(actorId);
                if (actor != null)
                    actorNames.Add(actor.Name);
            }

            var movieDto = new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                DirectorId = movie.DirectorId,
                DirectorName = director?.Name, // ✅ Fetch director name
                ScreenWriterId = movie.ScreenWriterId,
                ScreenWriterName = screenWriter?.Name, // ✅ Fetch screenwriter name
                ActorIds = actorIds,
                ActorNames = actorNames // ✅ Fetch actor names
            };

            movieDtos.Add(movieDto);
        }
        return movieDtos;
    }


    // Retrieve a specific movie by ID, including actors
    public async Task<MovieDTO> GetMovieByIdAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null) return null;

        var actorIds = await _moviesActorsRepository.GetActorsByMovieIdAsync(id);
        return new MovieDTO
        {
            Id = movie.Id,
            Title = movie.Title,
            Year = movie.Year,
            DirectorId = movie.DirectorId,
            ScreenWriterId = movie.ScreenWriterId,
            ActorIds = actorIds
        };
    }

    // Add a new movie and set actor relationships
    public async Task AddMovieAsync(string title, int year, int directorId, int screenWriterId, List<int> actorIds)
    {
        var newMovie = new Movie
        {
            Title = title,
            Year = year,
            DirectorId = directorId,
            ScreenWriterId = screenWriterId
        };

        // Insert movie and get generated ID
        int movieId = await _movieRepository.InsertAsync(newMovie);

        // Link actors to the movie in the join table
        foreach (var actorId in actorIds)
        {
            await _moviesActorsRepository.AddActorToMovieAsync(movieId, actorId);
        }
    }

    // Update a movie and manage actor relationships
    public async Task UpdateMovieAsync(int id, string newTitle, int newYear, int newDirectorId, int newScreenWriterId, List<int> newActorIds)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null) return;

        // Update movie details
        movie.Title = newTitle;
        movie.Year = newYear;
        movie.DirectorId = newDirectorId;
        movie.ScreenWriterId = newScreenWriterId;
        await _movieRepository.UpdateAsync(movie);

        // Fetch existing actor associations
        var existingActorIds = new HashSet<int>(await _moviesActorsRepository.GetActorsByMovieIdAsync(id));
        var newActorIdsSet = new HashSet<int>(newActorIds);

        // Remove actors that are no longer associated
        foreach (var actorId in existingActorIds.Except(newActorIdsSet))
        {
            await _moviesActorsRepository.RemoveActorFromMovieAsync(id, actorId);
        }

        // Add new actors that were not previously associated
        foreach (var actorId in newActorIdsSet.Except(existingActorIds))
        {
            await _moviesActorsRepository.AddActorToMovieAsync(id, actorId);
        }
    }

    // Delete a movie and its actor relationships
    public async Task DeleteMovieAsync(int id)
    {
        // Remove all actor relations first
        await _moviesActorsRepository.RemoveAllActorsFromMovieAsync(id);

        // Delete the movie itself
        await _movieRepository.DeleteAsync(id);
    }
}
