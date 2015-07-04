using System;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Core
{  
    public interface IExternalClientRepository : IEntityRepository<ExternalClient>
    {
        bool IsClientKeyValid(Guid clientKey);
    }
}