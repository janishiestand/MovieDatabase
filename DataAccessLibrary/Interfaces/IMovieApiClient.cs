using System;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Interfaces
{
	public interface IMovieApiClient
	{
        Task<OMBdSearchResult> SearchMovies(string searchQuery);
    }
}

