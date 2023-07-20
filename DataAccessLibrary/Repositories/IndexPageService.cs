using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;


namespace DataAccessLibrary.Repositories
{
	public class IndexPageService : IIndexPageService
	{
		private readonly IMovieRepository _movieRepository;

        public IndexPageService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IReadOnlyList<MovieViewModel>> GetIndexPageViewModelsAsync(string sortBy, bool isAscending, string SelectedFilter, string filterValue, CancellationToken cancellationToken)
		{
			IQueryable<Movie> moviesQuery = await _movieRepository.QueryAllMoviesAsync(cancellationToken);
			var indexPageViewModel = new List<MovieViewModel>();

            if (!string.IsNullOrEmpty(SelectedFilter) && !string.IsNullOrEmpty(filterValue))
            {
                moviesQuery = await _movieRepository.ApplyFilter(moviesQuery, SelectedFilter, filterValue, cancellationToken);
            }
            moviesQuery = await _movieRepository.ApplySorting(moviesQuery, sortBy, isAscending);

            indexPageViewModel = await CreateIndexPageViewModel(moviesQuery, indexPageViewModel);
            
            return indexPageViewModel;
		}

        public async Task<IReadOnlyList<MovieViewModel>> SortIndexView(string sortBy, bool isAscending, CancellationToken cancellationToken)
        {
            IQueryable<Movie> moviesQuery = await _movieRepository.QueryAllMoviesAsync(cancellationToken);
            var indexPageViewModel = new List<MovieViewModel>();
            moviesQuery = await _movieRepository.ApplySorting(moviesQuery, sortBy, isAscending);
            return await CreateIndexPageViewModel(moviesQuery, indexPageViewModel);
        }

        public async Task<List<MovieViewModel>> CreateIndexPageViewModel(IQueryable<Movie> moviesQuery, List<MovieViewModel> indexList)
        {
            foreach (var movie in moviesQuery)
            {
                indexList.Add(new MovieViewModel(movie.id, movie.MovieName, movie.Duration, movie.ReleaseDate, movie.Rating, movie.Actors));
            }
            return indexList;
        }

        public async Task AddMovie(string MovieName, int Duration, DateTime ReleaseDate, int Rating, List<ActorViewModel> ActorsViewModel, CancellationToken cancellationToken)
        {
            List<Actor> actors = ActorsViewModel
            .Select(a => new Actor(a.FirstName, a.LastName, a.Birthday))
            .ToList();

            Movie movie = new(
                MovieName: MovieName,
                Duration: Duration,
                ReleaseDate: ReleaseDate,
                Rating: Rating,
                Actors: actors
                );
            await _movieRepository.AddAsync(movie, cancellationToken);
            await _movieRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteMovie(int id, CancellationToken cancellationToken)
        {
            Movie? toRemove = await _movieRepository.FindAsync(id, cancellationToken);
            _movieRepository.Delete(toRemove);
            await _movieRepository.SaveChangesAsync(cancellationToken);
        }

    }
}

