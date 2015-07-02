namespace SunLine.Manager.Entities.Core
{
    public class Game : BaseEntity
    {        
        public Team HostTeam { get; set; }
		public Team GuestTeam { get; set; }
        public int HostTeamGoals { get; set; }
        public int GuestTeamGoals { get; set; }
        public League League { get; set; }
		public GameResultEnum GameResultEnum { get; set; }
        public GameStatusEnum GameStatusEnum { get; set; }
        
        public override string ToString()
        {
            string value = $"{HostTeam} - {GuestTeam}";
            switch(GameStatusEnum)
            {
                case GameStatusEnum.Scheduled:
                    value = $"{HostTeam} - {GuestTeam} (scheduled)";
                    break;
                case GameStatusEnum.InProgress:
                    value = $"{HostTeam} - {GuestTeam} (in progress)";
                    break;
                case GameStatusEnum.Finished:
                    value = $"{HostTeam} - {GuestTeam} ({HostTeamGoals} : {GuestTeamGoals})";
                    break; 
            }
            
            return value;
        }
    }
}
