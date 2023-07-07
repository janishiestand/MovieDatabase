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
		public Task<Movie> SearchMovieByTitle(string movieTitle, CancellationToken cancellationToken);
		public Task<Movie> ConvertSearchResult(OMBdSearchResult movieQuery, CancellationToken cancellationToken);
    }   
}

