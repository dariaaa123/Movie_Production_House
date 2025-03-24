using Microsoft.AspNetCore.Components.Web;
using Tema1PS.Model;
using Tema1PS.Model.Repositories;
using Tema1PS.Model.RepositoryPack;

namespace Tema1PS.Presenter
{
    public class MoviePresenter
    {
        private readonly MovieRepository _movieRepository;
        private readonly MoviesActorsRepository _moviesActorsRepository;
        private readonly DirectorRepository _directorRepository;
        private readonly ScreenWriterRepository _screenWriterRepository;
        private readonly ActorRepository _actorRepository;
        private IMovieGUI _movieGUI;

        public MoviePresenter(MovieRepository movieRepository, MoviesActorsRepository moviesActorsRepository,
            DirectorRepository directorRepository, ScreenWriterRepository screenWriterRepository,
            ActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _moviesActorsRepository = moviesActorsRepository;
            _directorRepository = directorRepository;
            _screenWriterRepository = screenWriterRepository;
            _actorRepository = actorRepository;
        }

        public void SetMovieGUI(IMovieGUI movieGUI)
        {
            _movieGUI = movieGUI ?? throw new ArgumentNullException(nameof(movieGUI));
        }

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
                    Category = movie.Category, // ✅ Ensure Category is included
                    Type = movie.Type, // ✅ Ensure Type is included
                    DirectorId = movie.DirectorId,
                    DirectorName = director?.Name,
                    ScreenWriterId = movie.ScreenWriterId,
                    ScreenWriterName = screenWriter?.Name,
                    ActorIds = actorIds,
                    ActorNames = actorNames
                };

                movieDtos.Add(movieDto);
            }
            return movieDtos;
        }

        public async Task AddMovieAsync()
        {
            var newMovie = new Movie
            {
                Title = _movieGUI.GetMovieTitle(),
                Year = _movieGUI.GetMovieYear(),
                Category = _movieGUI.GetMovieCategory(),
                Type = _movieGUI.GetMovieType(),
                DirectorId = _movieGUI.GetMovieDirectorId(),
                ScreenWriterId = _movieGUI.GetMovieScreenWriterId(),
            };

            int movieId = await _movieRepository.InsertAsync(newMovie);
            var actorIds = _movieGUI.GetMovieActorIds();

            foreach (var actorId in actorIds)
            {
                await _moviesActorsRepository.AddActorToMovieAsync(movieId, actorId);
            }
        }

        public async Task UpdateMovieAsync()
        {
            int movieId = _movieGUI.GetMovieId();

            // Retrieve the existing movie from the database to avoid tracking issues
            var existingMovie = await _movieRepository.GetByIdAsync(movieId);
            if (existingMovie == null)
            {
                throw new InvalidOperationException($"Movie with ID {movieId} not found.");
            }

            // Update the movie properties
            existingMovie.Title = _movieGUI.GetMovieTitle();
            existingMovie.Year = _movieGUI.GetMovieYear();
            existingMovie.Category = _movieGUI.GetMovieCategory();
            existingMovie.Type = _movieGUI.GetMovieType();
            existingMovie.DirectorId = _movieGUI.GetMovieDirectorId();
            existingMovie.ScreenWriterId = _movieGUI.GetMovieScreenWriterId();

            // Update the movie in the database
            await _movieRepository.UpdateAsync(existingMovie);

            // Update actors
            await _moviesActorsRepository.RemoveAllActorsFromMovieAsync(movieId);
            var actorIds = _movieGUI.GetMovieActorIds();
            foreach (var actorId in actorIds)
            {
                await _moviesActorsRepository.AddActorToMovieAsync(movieId, actorId);
            }
        }


        public async Task DeleteMovieAsync()
        {
            int id = _movieGUI.GetDeleteMovieId();
                
            await _moviesActorsRepository.RemoveAllActorsFromMovieAsync(id);
            await _movieRepository.DeleteAsync(id);
        }
    }
}
