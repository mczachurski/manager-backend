using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Core
{   
    public class TeamRepository : EntityRepository<Team>, ITeamRepository
    {
        public TeamRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
    }
}
