using System;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Core
{  
    public interface IUserSessionRepository : IEntityRepository<UserSession>
    {
        UserSession FindByAccessToken(Guid accessToken);
        bool IsActiveAccessToken(Guid accessToken);
    }
}