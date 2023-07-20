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
                indexList.Add(new ActorViewModel(actor.ActorId, actor.ActorFirstName, actor.ActorLastName, actor.Birthday));
            }
            return indexList;
        }
    }
}

