using System;
using System.Linq;
using SunLine.Manager.Entities.System;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.System
{  
    public class UserSessionRepository : EntityRepository<UserSession>, IUserSessionRepository
    {
        public UserSessionRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
        
        public UserSession FindByAccessToken(Guid accessToken)
        {
            return _databaseContext.UserSessions.FirstOrDefault(x => x.AccessToken == accessToken);
        }
        
        public bool IsActiveAccessToken(Guid accessToken)
        {
            return _databaseContext.UserSessions.Any(x => x.AccessToken == accessToken && x.IsActive);
        }
    }
}