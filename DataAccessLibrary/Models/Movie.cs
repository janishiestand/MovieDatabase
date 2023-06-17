using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
	public class Movie
	{
		public Movie(string MovieName, int Duration, DateTime ReleaseDate, int Rating)
		{
			this.MovieName = MovieName;
			this.Duration = Duration;
			this.ReleaseDate = ReleaseDate;
			this.Rating = Rating;
		}
			
		public int Id { get; set; }

		[Required]
		public string MovieName { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

		public List<Actor> Actors { get; set; } = new List<Actor>();
		public String? Director { get; set; }
		public int Rating { get; set; }

	}
}

