using System.Collections.Generic;
using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Football
{  
    public interface ITeamRepository : IEntityRepository<Team>
    {
        IList<PlayerPositionEnum> GetPlayerPositionsForTeamSetup(TeamSetupEnum teamSetupEnum);
    }
}