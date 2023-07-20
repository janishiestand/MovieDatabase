using System;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories
{
	public class SearchPageService : ISearchPageService
	{
		private readonly IMovieRepository _movieRepository;
        public IList<Movie> Movies { get; private set; } = new List<Movie>();

        public SearchPageService(IMovieRepository movieRepository)
		{
			_movieRepository = movieRepository;
		}

		public async Task<IReadOnlyList<MovieViewModel>> PerformSearchService(string MovieName, string? ReleaseYear, bool addToDatabase, CancellationToken cancellationToken)
		{
            OMBdSearchResult query = await _movieRepository.SearchMovieByTitle(MovieName, ReleaseYear, cancellationToken);
            if (query == null)
            {
                throw new MovieNotFound();
            }
            if (query.Response != "False")
            {
                Movie m = await _movieRepository.ConvertSearchResult(query, cancellationToken);

                if (addToDatabase)
                {
                    if (await _movieRepository.ContainsMovie(m))
                    {
                        throw new MovieAlreadyInDatabase();
                    }
                    await _movieRepository.AddAsync(m, cancellationToken);
                    await _movieRepository.SaveChangesAsync(cancellationToken);
                    Movies = await _movieRepository.GetAllMoviesAsync();
                }
                else
                {
                    Movies.Add(m);
                }
            }
            IReadOnlyList<MovieViewModel> movies = CreateIndexPageViewModel(Movies);
            return movies;
        }

        public class MovieNotFound : Exception
        {
            public MovieNotFound() : base("Movie not found.") { }
        }

        public class MovieAlreadyInDatabase : Exception
        {
            public MovieAlreadyInDatabase() : base("This movie has already been added to the library.") { }
        }

        public IReadOnlyList<MovieViewModel> CreateIndexPageViewModel(IList<Movie> moviesQuery)
        {
            var indexList = new List<MovieViewModel>();
            foreach (var movie in moviesQuery)
            {
                indexList.Add(new MovieViewModel(movie.id, movie.MovieName, movie.Duration, movie.ReleaseDate, movie.Rating, movie.Actors));
            }
            return indexList;
        }


    }
}

