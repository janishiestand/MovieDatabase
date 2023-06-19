﻿using System;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace DataAccessLibrary.Interfaces
{
	public interface IMovieRepository : IGenericRepository<Movie>
	{
		public Task<List<Movie>> GetAllMoviesAsync(CancellationToken cancellationToken);
        public Task<Movie?> FindAsync(int id, CancellationToken cancellationToken);
	}
}

