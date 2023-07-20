using System;
namespace DataAccessLibrary.Interfaces
{
	public interface IActorServicePage
	{
        public Task<IReadOnlyList<ActorViewModel>> ActorsByMovieID(int id, CancellationToken cancellationToken);
    }
}

