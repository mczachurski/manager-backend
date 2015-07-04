namespace SunLine.Manager.Entities.Football
{
    public class LeaguePosition : BaseEntity
    {        
        public League League { get; set; }
        public Team Team { get; set; }
		public int Order { get; set; }
		public int Points { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Lost { get; set; }
        public int GoalsScored { get ;set; }
        public int GoalsLost { get; set; }
		
        public override string ToString()
        {
            return $"{Team} : {Order}";
        }
    }
}
