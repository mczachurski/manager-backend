using System.Collections.Generic;

namespace SunLine.Manager.Entities.Core
{
    public class League : BaseEntity
    {
        public League()
        {
            Teams = new List<Team>();
        }
        
        public string Name { get; set; }
        public Season Season { get; set; }
        public IList<Team> Teams { get; set; }
		
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
