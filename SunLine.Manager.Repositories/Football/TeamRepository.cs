using System;
using System.Collections.Generic;
using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Football
{   
    public class TeamRepository : EntityRepository<Team>, ITeamRepository
    {
        public TeamRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
        
        public IList<PlayerPositionEnum> GetPlayerPositionsForTeamSetup(TeamSetupEnum teamSetupEnum)
		{
			switch(teamSetupEnum)
			{
				case TeamSetupEnum.Setup442A:
					return new List<PlayerPositionEnum>() 
					{ 
						PlayerPositionEnum.CentreForward, 
						PlayerPositionEnum.CentreForward,
						
						PlayerPositionEnum.CentreMidfield,
						PlayerPositionEnum.CentreMidfield,
						PlayerPositionEnum.LeftMidfield,
						PlayerPositionEnum.RightMidfield,
						
						PlayerPositionEnum.CentreBack,
						PlayerPositionEnum.CentreBack,
						PlayerPositionEnum.LeftBack,
						PlayerPositionEnum.RightBack
					};
				case TeamSetupEnum.Setup442B:
					return new List<PlayerPositionEnum>() 
					{ 
						PlayerPositionEnum.CentreForward, 
						PlayerPositionEnum.CentreForward,
						
						PlayerPositionEnum.OffensiveMidfield,
						PlayerPositionEnum.CentreMidfield,
						PlayerPositionEnum.CentreMidfield,
						PlayerPositionEnum.DefensiveMidfield,
						
						PlayerPositionEnum.CentreBack,
						PlayerPositionEnum.CentreBack,
						PlayerPositionEnum.LeftBack,
						PlayerPositionEnum.RightBack
					};
				case TeamSetupEnum.Setup433A:
					return new List<PlayerPositionEnum>() 
					{ 
						PlayerPositionEnum.LeftForward, 
						PlayerPositionEnum.CentreForward,
						PlayerPositionEnum.RightForward,
						
						PlayerPositionEnum.CentreMidfield,
						PlayerPositionEnum.CentreMidfield,
						PlayerPositionEnum.CentreMidfield,
						
						PlayerPositionEnum.CentreBack,
						PlayerPositionEnum.CentreBack,
						PlayerPositionEnum.LeftBack,
						PlayerPositionEnum.RightBack
					};
				default:
					throw new NotImplementedException("This team setup is not implemented.");
			}
		}
    }
}
