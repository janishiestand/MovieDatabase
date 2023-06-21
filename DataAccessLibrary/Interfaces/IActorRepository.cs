using System;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Interfaces
{
    public interface IActorRepository : IGenericRepository<Actor>
    {
        public Task<int> GetMovieIdByActorId(int id, CancellationToken cancellationToken);
    }
}

