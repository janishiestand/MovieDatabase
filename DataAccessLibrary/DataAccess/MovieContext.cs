using System;
using Microsoft.EntityFrameworkCore;
using DataAccessLibrary.Models;
using MySqlConnector;
using Pomelo.EntityFrameworkCore;

namespace DataAccessLibrary.DataAccess
{
	public class MovieContext : DbContext
	{
        public MovieContext(DbContextOptions<MovieContext> options): base(options) { }
		public DbSet<Movie> Movie { get; set; }
		public DbSet<Actor> Actor { get; set; }
    }
}

