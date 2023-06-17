using System;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace DataAccessLibrary.Interfaces
{
	public interface IMovieRepository : IGenericRepository<Movie>
	{
		Task<List<Movie>> GetAllMoviesAsync(CancellationToken cancellationToken);
	}
}

