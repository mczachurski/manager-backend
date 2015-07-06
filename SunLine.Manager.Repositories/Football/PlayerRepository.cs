using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Football
{   
    public class PlayerRepository : EntityRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
    }
}
