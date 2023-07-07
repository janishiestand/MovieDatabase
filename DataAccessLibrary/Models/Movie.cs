using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DataAccessLibrary.Models
{
	public class Movie
	{
		public int id { get; set; }

		[Required]
        [JsonPropertyName("Title")]
        public string MovieName { get; set; }

        [Required]
        [JsonProperty("Runtime")]
        public int Duration { get; set; }

        [Required]
        [JsonProperty("Released")]
        public DateTime ReleaseDate { get; set; }

		[Required]
        [JsonProperty("Ratings")]
        public int Rating { get; set; }

        public List<Actor>? Actors { get; set; } = new List<Actor>();
		public String? Director { get; set; }

        public Movie() { }

        public Movie(string MovieName, int Duration, DateTime ReleaseDate, int Rating)
        {
            this.MovieName = MovieName;
            this.Duration = Duration;
            this.ReleaseDate = ReleaseDate;
            this.Rating = Rating;
        }

    }
}

