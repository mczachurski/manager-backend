using System;
using System.Linq;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Infrastructure;

namespace SunLine.Manager.Repositories.Core
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
    }
}