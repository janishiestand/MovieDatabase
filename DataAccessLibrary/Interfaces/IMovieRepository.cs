using System;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace DataAccessLibrary.Interfaces
{
	public interface IMovieRepository : IGenericRepository<Movie>
	{
		Task<List<Movie>> GetAllMoviesAsync(CancellationToken cancellationToken);
		Task AddRangeAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken);
		Task<int> CountAsync(CancellationToken cancellationToken);
	}
}

