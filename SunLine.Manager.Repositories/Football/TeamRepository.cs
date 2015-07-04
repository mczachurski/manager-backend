using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Football
{   
    public class TeamRepository : EntityRepository<Team>, ITeamRepository
    {
        public TeamRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
    }
}
