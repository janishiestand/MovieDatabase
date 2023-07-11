using System;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace DataAccessLibrary.Interfaces
{
	public interface IMovieRepository : IGenericRepository<Movie>
	{
		public Task<List<Movie>> GetAllMoviesAsync(CancellationToken cancellationToken);
		public Task AddRangeAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken);
		public Task<List<Actor>> GetActorsByMovieID(int id, CancellationToken cancellationToken);
		public Task<Movie> FirstOrDefaultAsync(int id, CancellationToken cancellationToken);
		public Task<OMBdSearchResult> SearchMovieByTitle(string movieTitle, string year, CancellationToken cancellationToken);
		public Task<Movie> ConvertSearchResult(OMBdSearchResult movieQuery, CancellationToken cancellationToken);
		public new Task<Movie?> FindAsync(int id, CancellationToken cancellationToken);
		public Task<IQueryable<Movie>> QueryAllMoviesAsync(CancellationToken cancellationToken);
		public IQueryable<Movie> QueryAllMovies(CancellationToken cancellationToken);
    }   
}

