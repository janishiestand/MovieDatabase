using System;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Interfaces
{
	public interface IIndexPageService
	{
		public Task<IReadOnlyList<MovieViewModel>> GetIndexPageViewModelsAsync(string sortBy, bool isAscending, string selectedFilter, string filterValue, CancellationToken cancellationToken);
		public Task AddMovie(string MovieName, int Duration, DateTime ReleaseDate, int Rating, List<ActorViewModel> ActorsViewModel, CancellationToken cancellationToken);
		public Task DeleteMovie(int id, CancellationToken cancellationToken);
    }

    public record MovieViewModel(int id, string MovieName, int Duration, DateTime ReleaseDate, int Rating, List<Actor> Actors);
	public record ActorViewModel(string FirstName, string LastName, DateTime Birthday);
}

