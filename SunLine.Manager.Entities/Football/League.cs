using System.Collections.Generic;

namespace SunLine.Manager.Entities.Football
{
    public class League : BaseEntity
    {
        public League()
        {
            Teams = new List<Team>();
            LeaguePositions = new List<LeaguePosition>();
        }
        
        public string Name { get; set; }
        public Season Season { get; set; }
        public IList<Team> Teams { get; set; }
        public IList<LeaguePosition> LeaguePositions { get; set; }
        
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
