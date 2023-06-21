using System;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using MySqlConnector;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repositories
{
	public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
		private readonly MovieContext _context;

		public ActorRepository(MovieContext context) : base (context)
		{
			_context = context;

        }

    }
}

