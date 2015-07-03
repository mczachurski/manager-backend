using System;
using SunLine.Manager.Entities.System;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.System
{  
    public class ExternalClientRepository : EntityRepository<ExternalClient>, IExternalClientRepository
    {
        public ExternalClientRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
        
        public bool IsClientKeyValid(Guid clientKey)
        {
            return true;
        }
    }
}