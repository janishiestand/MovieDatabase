using System;
using Microsoft.EntityFrameworkCore;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.DataAccess
{
	public class MovieContext : DbContext
	{
        public MovieContext(DbContextOptions<MovieContext> options): base(options) { }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Actor> Actors { get; set; }
   }
}

