using System;
using System.Text.Json;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DataAccessLibrary.DataAccess
{
	public class MovieApiClient : IMovieApiClient
	{
		private readonly HttpClient _httpClient;

		public MovieApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<OMBdSearchResult> SearchMovies(string searchQuery, string? releaseYear, CancellationToken cancellationToken)
		{
			if (searchQuery == null)
			{
				return null;
			}
			string apiKey = "3a857115";
            string apiUrl = $"http://www.omdbapi.com/?apikey={apiKey}&t={Uri.EscapeDataString(searchQuery)}";

            if (releaseYear != null)
			{
				apiUrl = apiUrl + $"&y={Uri.EscapeDataString(releaseYear)}";
            }

            var response = await _httpClient.GetAsync(apiUrl);
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				
				OMBdSearchResult searchResult = JsonSerializer.Deserialize<OMBdSearchResult>(json);

                return searchResult;
			}
			
			else {
                return null;
            }
			
        }
	}
}

