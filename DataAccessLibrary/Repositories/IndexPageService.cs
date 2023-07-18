using System;
using System.Globalization;
using System.Threading;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;


namespace DataAccessLibrary.Repositories
{
	public class IndexPageService : IIndexPageService
	{
		private readonly IMovieRepository _movieRepository;
		private readonly IActorRepository _actorRepository;

        public IndexPageService(IMovieRepository movieRepository, IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        public async Task<IReadOnlyList<IndexPageViewModel>> GetIndexPageViewModelsAsync(string sortBy, bool isAscending, string SelectedFilter, string filterValue, CancellationToken cancellationToken)
		{
			IQueryable<Movie> moviesQuery = await _movieRepository.QueryAllMoviesAsync(cancellationToken);
			var indexPageViewModel = new List<IndexPageViewModel>();

            if (!string.IsNullOrEmpty(SelectedFilter) && !string.IsNullOrEmpty(filterValue))
            {
                moviesQuery = await _movieRepository.ApplyFilter(moviesQuery, SelectedFilter, filterValue, cancellationToken);
            }
            moviesQuery = await _movieRepository.ApplySorting(moviesQuery, sortBy, isAscending);

            indexPageViewModel = await CreateIndexPageViewModel(moviesQuery, indexPageViewModel);
            
            return indexPageViewModel;
		}

        public async Task<IReadOnlyList<IndexPageViewModel>> SortIndexView(string sortBy, bool isAscending, CancellationToken cancellationToken)
        {
            IQueryable<Movie> moviesQuery = await _movieRepository.QueryAllMoviesAsync(cancellationToken);
            var indexPageViewModel = new List<IndexPageViewModel>();
            moviesQuery = await _movieRepository.ApplySorting(moviesQuery, sortBy, isAscending);
            return await CreateIndexPageViewModel(moviesQuery, indexPageViewModel);
        }

        public async Task<List<IndexPageViewModel>> CreateIndexPageViewModel(IQueryable<Movie> moviesQuery, List<IndexPageViewModel> indexList)
        {
            foreach (var movie in moviesQuery)
            {
                indexList.Add(new IndexPageViewModel(movie.id, movie.MovieName, movie.Duration, movie.ReleaseDate, movie.Rating, movie.Actors));
            }
            return indexList;
        }

        public async Task AddMovie(string MovieName, int Duration, DateTime ReleaseDate, int Rating, List<IndexActorViewModel> ActorsViewModel, CancellationToken cancellationToken)
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

