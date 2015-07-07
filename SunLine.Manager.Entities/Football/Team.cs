using System.Collections.Generic;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.Dict;

namespace SunLine.Manager.Entities.Football
{
	public class Team : BaseEntity
	{
		public Team()
		{
			Players = new List<Player>();
			Leagues = new List<League>();
		}
		
		public string Name { get; set; }
		public User User { get; set; }
		public int UserId { get; set; }
		public IList<Player> Players { get; set; }
		public IList<League> Leagues { get; set; }
		public League CurrentLeague { get; set;}
		public LeaguePosition CurrentLeaguePosition { get; set; }
		public double Budget { get; set; }
		public Stadium Stadium { get; set; }
		public int StadiumId { get; set; }
		public TeamSetupEnum TeamSetup { get; set; }
		
		public override string ToString()
		{
			return $"{Name}";
		}
	}
}