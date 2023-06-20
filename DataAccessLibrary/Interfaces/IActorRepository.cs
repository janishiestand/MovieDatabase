using System;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Interfaces
{
    public interface IActorRepository : IGenericRepository<Actor>
    {
        // public Task<Movie> GetMovieId(int id, CancellationToken cancellationToken);
    }
}

