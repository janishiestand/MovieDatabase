using System;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Interfaces
{
	public interface IIndexPageService
	{
		public Task<IReadOnlyList<IndexPageViewModel>> GetIndexPageViewModelsAsync(string sortBy, bool isAscending, string selectedFilter, string filterValue, CancellationToken cancellationToken);
		public Task AddMovie(string MovieName, int Duration, DateTime ReleaseDate, int Rating, List<IndexActorViewModel> ActorsViewModel, CancellationToken cancellationToken);
		public Task DeleteMovie(int id, CancellationToken cancellationToken);
    }

    public record IndexPageViewModel(int id, string MovieName, int Duration, DateTime ReleaseDate, int Rating, List<Actor> Actors);
	public record IndexActorViewModel(string FirstName, string LastName, DateTime Birthday);
}

