using System;
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

        public async Task<List<Movie>> GetAllMoviesAsync(CancellationToken cancellationToken)
		{
			return await _context.Movies.Include(e => e.Actors).ToListAsync(cancellationToken);
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
		
        public async Task<OMBdSearchResult> SearchMovieByTitle(string movieTitle, string year, CancellationToken cancellationToken)
        {
			OMBdSearchResult movieQuery = await _movieApiClient.SearchMovies(movieTitle, year, cancellationToken);
            return movieQuery;
        }

		public async Task<Movie> ConvertSearchResult(OMBdSearchResult movieQuery, CancellationToken cancellationToken)
		{
			string runtimeVal = Regex.Match(movieQuery.Runtime, @"\d+").Value;
			int rt;;
			if (!int.TryParse(runtimeVal, out rt)) { rt = 0; }
            DateTime dateTime = DateTime.Parse(movieQuery.Released);
            Rating imdbRating = movieQuery.Ratings.FirstOrDefault(r => r.Source == "Internet Movie Database");
            int ratingVal = imdbRating?.GetOMBdRating() ?? 0;

			List<Actor> QueriedActors = movieQuery.Actors.Split(',').Select(a =>
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

			Movie requestedMovie = new Movie
			(
				MovieName: movieQuery.Title,
				Duration: rt,
				ReleaseDate: dateTime,
				Rating: ratingVal,
				Actors: QueriedActors
			);

			return requestedMovie;
        }
    }
}

