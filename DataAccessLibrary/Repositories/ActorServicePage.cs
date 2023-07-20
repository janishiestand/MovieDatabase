using System;
using System.Threading;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repositories
{
	public class ActorServicePage : IActorServicePage
	{
		private readonly IActorRepository _actorRepository;
		private readonly IMovieRepository _movieRepository;
		public IList<Actor> Actors { get; set; } = new List<Actor>();

		public ActorServicePage(IActorRepository actorRepository, IMovieRepository movieRepository)
		{
			_actorRepository = actorRepository;
			_movieRepository = movieRepository;
		}

		public async Task<IReadOnlyList<ActorViewModel>> ActorsByMovieID(int id, CancellationToken cancellationToken)
		{
			Actors = await _movieRepository.GetActorsByMovieID(id, cancellationToken);
			return CreateActorPageViewModel(Actors);
        }

        public IReadOnlyList<ActorViewModel> CreateActorPageViewModel(IList<Actor> actorsQuery)
        {
            var indexList = new List<ActorViewModel>();
            foreach (var actor in actorsQuery)
            {
                indexList.Add(new ActorViewModel(actor.ActorId, actor.ActorFirstName, actor.ActorLastName, actor.Birthday, actor.Movieid));
            }
            return indexList;
        }

		public async Task UpdateActor(ActorViewModel updatedActor,  CancellationToken cancellationToken)
		{
            Actor actor = await _actorRepository.FindAsync(updatedActor.id, cancellationToken);
            if (actor == null) { throw new Exception("Actor not found"); }

            actor.ActorFirstName = updatedActor.FirstName;
            actor.ActorLastName = updatedActor.LastName;
            actor.Birthday = updatedActor.Birthday;

            await _actorRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<ActorViewModel> FindActor(int id, CancellationToken cancellationToken)
        {
            Actor actor = await _actorRepository.FindAsync(id, cancellationToken);
            return await ConvertActorToViewModel(actor);
        }

        public async Task<ActorViewModel> ConvertActorToViewModel(Actor actor)
        {
            return new ActorViewModel(actor.ActorId, actor.ActorFirstName, actor.ActorLastName, actor.Birthday, actor.Movieid);
        }

        public async Task AddActorToMovie(ActorViewModel Actor, int MovieID, CancellationToken cancellationToken)
        {
            Actor ActorToAdd = new Actor(Actor.FirstName, Actor.LastName, Actor.Birthday, MovieID);
            await _actorRepository.AddAsync(ActorToAdd, cancellationToken);
            await _actorRepository.SaveChangesAsync(cancellationToken);
        }

    }
}

