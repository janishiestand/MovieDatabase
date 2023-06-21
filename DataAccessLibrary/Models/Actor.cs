using System;
namespace DataAccessLibrary.Models
{
	public class Actor
	{
		public int ActorId { get; set; }
		public String ActorFirstName { get; set; }
		public String ActorLastName { get; set; }
		public DateTime Birthday { get; set; }
		public int Movieid { get; set; }

		public Actor() { }

		public Actor(string ActorFirstName, string ActorLastName, DateTime Birthday)
		{
			this.ActorFirstName = ActorFirstName;
			this.ActorLastName = ActorLastName;
			this.Birthday = Birthday;
		}


	}
}

