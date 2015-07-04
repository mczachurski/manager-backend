using System.Collections.Generic;

namespace SunLine.Manager.Entities.Football
{
    public class Game : BaseEntity
    {        
        public Game()
        {
            GameEvents = new List<GameEvent>();
            Squad = new List<Player>();
        }
        
        public Team HostTeam { get; set; }
		public Team GuestTeam { get; set; }
        public int HostTeamGoals { get; set; }
        public int GuestTeamGoals { get; set; }
        public League League { get; set; }
		public GameResultEnum GameResult { get; set; }
        public GameStatusEnum GameStatus { get; set; }
        public IList<GameEvent> GameEvents { get; set; }
        public int Attendance { get; set; }
        public Referee Referee { get; set; }
        public int HostPossession { get; set; }
        public int GuestPossession { get; set; }
        public int HostShotsOnTarget { get; set; }
        public int GuestShotsOnTarget { get; set; } 
        public int HostShotsOffTarget { get; set; }
        public int GuestShotsOffTarget { get; set; }
        public int HostCorners { get; set; }
        public int GuestCorners { get; set; }
        public int HostPenalties { get; set; }
        public int GuestPenalties { get; set; }
        public int HostOffSides { get; set; }
        public int GuestOffsides { get; set; }
        public int HostFauls { get; set; }
        public int GuestFauls { get; set; }
        public int HostAccuratePasses { get; set; }
        public int GuestAccuratePasses { get; set; }
        public int HostNotAccuratePasses { get; set; }
        public int GuestNotAccuratePasses { get; set; }
        public int HostFreeKickShot { get; set; }
        public int GuestFreeKickShot { get; set; }
        public IList<Player> Squad { get; set; }
        
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
