using System;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repositories
{
	public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
		private readonly MovieContext _context;

		public MovieRepository(MovieContext context) : base (context)
		{
			_context = context;
		}

		public async Task<List<Movie>> GetAllMoviesAsync(CancellationToken cancellationToken)
		{
			return await _context.Movies.ToListAsync(cancellationToken);
		}

		public async Task<Movie?> FindAsync(int id, CancellationToken cancellationToken)
		{
			Movie? movie = await _context.Movies.FindAsync(id, cancellationToken);
			return movie;
		}
	}
}

