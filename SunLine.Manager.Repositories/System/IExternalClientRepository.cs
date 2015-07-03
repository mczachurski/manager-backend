using System;
using SunLine.Manager.Entities.System;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.System
{  
    public interface IExternalClientRepository : IEntityRepository<ExternalClient>
    {
        bool IsClientKeyValid(Guid clientKey);
    }
}