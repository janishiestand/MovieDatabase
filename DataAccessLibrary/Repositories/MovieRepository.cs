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

		public async Task<List<Movie>> GetAllMoviesAsync(CancellationToken cancellationToken)
		{
			return await _context.Movies.Include(e => e.Actors).ToListAsync(cancellationToken);
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
		
        public async Task<Movie> SearchMovieByTitle(string movieTitle, CancellationToken cancellationToken)
        {
			OMBdSearchResult movieQuery = await _movieApiClient.SearchMovies(movieTitle);
			Movie movie = await ConvertSearchResult(movieQuery, cancellationToken);
            return movie;
        }

		public async Task<Movie> ConvertSearchResult(OMBdSearchResult movieQuery, CancellationToken cancellationToken)
		{
            int rt = Int32.Parse(Regex.Match(movieQuery.Runtime, @"\d+").Value);
            DateTime dateTime = DateTime.Parse(movieQuery.Released);
            Rating imdbRating = movieQuery.Ratings.FirstOrDefault(r => r.Source == "Internet Movie Database");
            int ratingVal = imdbRating?.GetOMBdRating() ?? 0;

			Movie requestedMovie = new Movie(
			MovieName: movieQuery.Title,
			Duration: rt,
			ReleaseDate: dateTime,
			Rating: ratingVal
			);

			return requestedMovie;
        }
    }
}

