using System;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;

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

