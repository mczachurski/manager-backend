using System;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Core
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