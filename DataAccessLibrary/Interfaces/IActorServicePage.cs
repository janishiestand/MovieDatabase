using System;
namespace DataAccessLibrary.Interfaces
{
	public interface IActorServicePage
	{
        public Task<IReadOnlyList<ActorViewModel>> ActorsByMovieID(int id, CancellationToken cancellationToken);
        public Task UpdateActor(ActorViewModel updatedActor, CancellationToken cancellationToken);
        public Task<ActorViewModel> FindActor(int id, CancellationToken cancellationToken);
    }
}

