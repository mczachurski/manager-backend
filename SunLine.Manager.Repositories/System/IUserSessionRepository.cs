using System;
using SunLine.Manager.Entities.System;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.System
{  
    public interface IUserSessionRepository : IEntityRepository<UserSession>
    {
        UserSession FindByAccessToken(Guid accessToken);
        bool IsActiveAccessToken(Guid accessToken);
    }
}