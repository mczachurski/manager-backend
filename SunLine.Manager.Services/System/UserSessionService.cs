using System;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.System;
using SunLine.Manager.Repositories.System;

namespace SunLine.Manager.Services.System
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _userSessionRepository;

        public UserSessionService(IUserSessionRepository userSessionRepository)
        {
            _userSessionRepository = userSessionRepository;
        }

        public UserSession CreateUserSession(User user, Guid accessToken, string host)
        {
            var userSession = new UserSession
            {
                IsActive = true,
                SessionStart = DateTime.UtcNow,
                User = user,
                AccessToken = accessToken  
            };
            
            return _userSessionRepository.Create(userSession);
        }

        public void AbortUserSession(Guid accessToken)
        {
            UserSession userSession = _userSessionRepository.FindByAccessToken(accessToken);
            if (userSession != null)
            {
                userSession.IsActive = false;
                userSession.SessionEnd = DateTime.UtcNow;
                _userSessionRepository.Update(userSession);
            }
        }

        public UserSession FindByAccessToken(Guid accessToken)
        {
            return _userSessionRepository.FindByAccessToken(accessToken);
        }
		
		public bool IsActiveAccessToken(Guid accessToken)
        {
            return _userSessionRepository.IsActiveAccessToken(accessToken);
        }
    }
}