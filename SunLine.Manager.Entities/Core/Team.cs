using System.Collections.Generic;

namespace SunLine.Manager.Entities.Core
{
	public class Team : BaseEntity
	{
		public Team()
		{
			Players = new List<Player>();
		}
		
		public string Name { get; set; }
		public User User { get; set; }
		public IList<Player> Players { get; set; }
		
		public override string ToString()
		{
			return $"{Name}";
		}
	}
}