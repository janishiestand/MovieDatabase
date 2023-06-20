﻿using System;
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
			return await _context.Movies.Include(e => e.Actors).ToListAsync(cancellationToken);
		}

		public async Task<Movie?> FindAsync(int id, CancellationToken cancellationToken)
		{
			Movie? movie = await _context.Movies.FindAsync(id, cancellationToken);
			return movie;
		}

		public async Task AddRangeAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken)
		{
			await _context.AddRangeAsync(movies, cancellationToken);
		}

		public async Task<List<Actor>> GetActorsByMovie(Movie movie)
		{
			List<Actor> actors = movie.Actors.ToList();
			return actors;
		}

		public async Task<Movie> FirtOrDefaultAsync(int id)
		{
			Movie m = await _context.Movies.Include(e => e.Actors).FirstOrDefaultAsync(x => x.id == id);
			return m;
		}
		

    }
}

