using System;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Interfaces
{
	public interface ISearchPageService
	{
		public Task<IReadOnlyList<MovieViewModel>> PerformSearchService(string MovieName, string? ReleaseYear, bool addToDatabase, CancellationToken cancellationToken);
		public IReadOnlyList<MovieViewModel> CreateSearchPageViewModel(IList<Movie> moviesQuery);
    }

}

