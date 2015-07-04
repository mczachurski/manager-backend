using System.Collections.Generic;

namespace SunLine.Manager.Entities.Football
{
    public class Season : BaseEntity
    {
        public Season()
        {
            Leagues = new List<League>();
        }
        
        public string Name { get; set; }
        public IList<League> Leagues { get; set; }
		
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
