using System.Collections.Generic;
using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.Entities.Football
{
	public class Team : BaseEntity
	{
		public Team()
		{
			Players = new List<Player>();
		}
		
		public string Name { get; set; }
		public User User { get; set; }
		public int UserId { get; set; }
		public IList<Player> Players { get; set; }
		
		public override string ToString()
		{
			return $"{Name}";
		}
	}
}