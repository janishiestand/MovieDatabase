using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repositories
{
	public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
		private readonly MovieContext _context;
		private readonly IMovieApiClient _movieApiClient;

		public MovieRepository(MovieContext context, IMovieApiClient movieApiClient) : base (context)
		{
			_context = context;
			_movieApiClient = movieApiClient;
		}

        public new async Task<Movie?> FindAsync(int id, CancellationToken cancellationToken)
        {
			Movie? Movie = await _context.Movies.Include(e => e.Actors).FirstOrDefaultAsync(x => x.id == id, cancellationToken);
            return Movie;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
		{
			return await _context.Movies.Include(e => e.Actors).ToListAsync();
		}

        public async Task<IQueryable<Movie>> QueryAllMoviesAsync(CancellationToken cancellationToken)
        {

			return await Task.FromResult(QueryAllMovies(cancellationToken));
        }

        public IQueryable<Movie> QueryAllMovies(CancellationToken cancellationToken)
        {
            return _context.Movies.Include(e => e.Actors);
        }

        public async Task AddRangeAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken)
		{
			await _context.AddRangeAsync(movies, cancellationToken);
		}

        public async Task<List<Actor>> GetActorsByMovieID(int id, CancellationToken cancellationToken)
        {
			Movie movie = await FirstOrDefaultAsync(id, cancellationToken);
            List<Actor> actors = movie.Actors.ToList();
            return actors;
        }

        public async Task<Movie> FirstOrDefaultAsync(int id, CancellationToken cancellationToken)
		{
			Movie m = await _context.Movies.Include(e => e.Actors).FirstOrDefaultAsync(x => x.id == id, cancellationToken);
			return m;
		}
		
        public async Task<OMBdSearchResult> SearchMovieByTitle(string movieTitle, string? releaseYear, CancellationToken cancellationToken)
        {
			OMBdSearchResult movieQuery = await _movieApiClient.SearchMovies(movieTitle, releaseYear, cancellationToken);
            return movieQuery;
        }

		public async Task<Movie> ConvertSearchResult(OMBdSearchResult movieQuery, CancellationToken cancellationToken)
		{
			int runtimeVal = ParseDuration(movieQuery.Runtime);
            DateTime dateTime = ParseReleaseDate(movieQuery.Released);
            int ratingVal = ParseRating(movieQuery.Ratings);

			List<Actor> QueriedActors = CreateActorsFromQuery(movieQuery.Actors);

			Movie requestedMovie = new Movie
			(
				MovieName: movieQuery.Title,
				Duration: runtimeVal,
				ReleaseDate: dateTime,
				Rating: ratingVal,
				Actors: QueriedActors
			);

			return requestedMovie;
        }

		public List<Actor> CreateActorsFromQuery(string queriedActors)
		{
            return queriedActors.Split(',').Select(a =>
            {
                string[] nameParts = a.Trim().Split(' ');
                string firstName = nameParts[0];
                string lastName = (nameParts.Length > 1) ? nameParts[1] : string.Empty;

                return new Actor
                {
                    ActorFirstName = firstName,
                    ActorLastName = lastName,
                    Birthday = DateTime.MinValue
                };
            }).ToList();
        }

		public int ParseRating(List<Rating> queriedRatings)
		{
			Rating rating = queriedRatings.FirstOrDefault(r => r.Source == "Internet Movie Database");
			return rating?.GetOMBdRating() ?? 0;
        }

		public DateTime ParseReleaseDate(string released)
		{
			return DateTime.Parse(released);

		}

		public int ParseDuration(string queriedRuntime)
		{
			string runtimeVal = Regex.Match(queriedRuntime, @"\d+").Value;
            int duration;
            if (!int.TryParse(runtimeVal, out duration)) { duration = 0; }
			return duration;
        }

		public async Task<IQueryable<Movie>> ApplySorting(IQueryable<Movie> moviesQuery, string sortBy, bool isAscending)
		{
            switch (sortBy)
            {
                case "title":
                    return (isAscending) ? moviesQuery.OrderBy(m => m.MovieName) : moviesQuery.OrderByDescending(m => m.MovieName);
                case "duration":
                    return (isAscending) ? moviesQuery.OrderBy(m => m.Duration) : moviesQuery.OrderByDescending(m => m.Duration);
                case "releaseDate":
                    return (isAscending) ? moviesQuery.OrderBy(m => m.ReleaseDate) : moviesQuery.OrderByDescending(m => m.ReleaseDate);
                case "rating":
                    return (isAscending) ? moviesQuery.OrderBy(m => m.Rating) : moviesQuery.OrderByDescending(m => m.Rating);
                default:
                    return moviesQuery.OrderBy(m => m.id);
            }
        }

        public async Task<IQueryable<Movie>> ApplyFilter(IQueryable<Movie> moviesQuery, string selectedFilter, string filterValue)
		{
			switch (selectedFilter)
			{
				case "title":
					moviesQuery = moviesQuery.Where(m => m.MovieName.Contains(filterValue));
					break;

				case "releaseDate":
					int releaseDate = Int32.Parse(filterValue);
					moviesQuery = moviesQuery.Where(m => m.ReleaseDate.Year == releaseDate);
					break;

				case "duration":
					int duration = int.Parse(filterValue);
					moviesQuery = moviesQuery.Where(m => m.Duration == duration);
					break;

				case "rating":
					int rating = int.Parse(filterValue);
					moviesQuery = moviesQuery.Where(m => m.Rating == rating);
					break;
			}
			return moviesQuery;
		}

		public async Task<bool> ContainsMovie(Movie Movie)
		{
            return await _context.Movies.AnyAsync(m => m.MovieName == Movie.MovieName && m.ReleaseDate == Movie.ReleaseDate);
        }
    }
}

