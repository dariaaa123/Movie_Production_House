using Tema1PS.Model;
using Tema1PS.Model.Repositories;
using Tema1PS.Model.RepositoryPack;
using Tema1PS.Presenter;

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
                    Category = movie.Category,
                    Type = movie.Type,
                    DirectorId = movie.DirectorId,
                    DirectorName = director.Name,
                    ScreenWriterId = movie.ScreenWriterId,
                    ScreenWriterName = screenWriter.Name,
                    ActorIds = actorIds,
                    ActorNames = actorNames,
                    Photo1 = movie.Photo1,
                    Photo2 = movie.Photo2,
                    Photo3 = movie.Photo3
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
                Photo1 = _movieGUI.GetMoviePhoto1(),
                Photo2 = _movieGUI.GetMoviePhoto2(),
                Photo3 = _movieGUI.GetMoviePhoto3()
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
            
            var existingMovie = await _movieRepository.GetByIdAsync(movieId);
            /*if (existingMovie == null)
            {
                throw new InvalidOperationException($"Movie with ID {movieId} not found.");
            }*/

            
            existingMovie.Title = _movieGUI.GetMovieTitle();
            existingMovie.Year = _movieGUI.GetMovieYear();
            existingMovie.Category = _movieGUI.GetMovieCategory();
            existingMovie.Type = _movieGUI.GetMovieType();
            existingMovie.DirectorId = _movieGUI.GetMovieDirectorId();
            existingMovie.ScreenWriterId = _movieGUI.GetMovieScreenWriterId();

         
            existingMovie.Photo1 = _movieGUI.GetMoviePhoto1();
            existingMovie.Photo2 = _movieGUI.GetMoviePhoto2();
            existingMovie.Photo3 = _movieGUI.GetMoviePhoto3();

       
            await _movieRepository.UpdateAsync(existingMovie);

        
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

        public async Task<List<ActorDTO>> GetActorsAsync()
        {
            var actors = await _actorRepository.GetAllAsync(); 

            return actors.Select(a => new ActorDTO
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        }
        
        public async Task<List<DirectorDTO>> GetDirectorsAsync()
        {
            var directors = await _directorRepository.GetAllAsync(); 

            return directors.Select(d => new DirectorDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();
        }

        
        public async Task<List<ScreenWriterDTO>> GetScreenwritersAsync()
        {
            var screenwriters = await _screenWriterRepository.GetAllAsync(); 

            return screenwriters.Select(s => new ScreenWriterDTO
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }


    }
}