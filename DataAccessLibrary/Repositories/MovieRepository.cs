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

		public async Task<Movie> GetAllMovies()
		{
			return await GetAll().OrderByDescending(x => x.MovieName).FirstOrDefaultAsync();
		}
	}
}

