using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Football
{   
    public class StadiumRepository : EntityRepository<Stadium>, IStadiumRepository
    {
        public StadiumRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
    }
}
