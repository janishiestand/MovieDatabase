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

		public async Task<int> GetMovieIdByActorId(int id, CancellationToken cancellationToken)
		{
			//Actor a = await _context.Actors.FindAsync(id);
			return await _context.Actors.Where(a => a.ActorId == id).Select(m => m.Movieid).FirstOrDefaultAsync(cancellationToken);
			//return a.Movieid;
		}

    }
}

