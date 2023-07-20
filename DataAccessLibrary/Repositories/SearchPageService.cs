using System;
using DataAccessLibrary.Interfaces;

namespace DataAccessLibrary.Repositories
{
	public class SearchPageService : ISearchPageService
	{
		private readonly IMovieRepository _movieRepository;

		public SearchPageService(IMovieRepository movieRepository)
		{
			_movieRepository = movieRepository;
		}


	}
}

