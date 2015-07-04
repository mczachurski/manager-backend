using System;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.Services.Core
{
	public interface IUserSessionService
	{
        UserSession CreateUserSession(User user, string host);
        void AbortUserSession(Guid accessToken);
        UserSession FindByAccessToken(Guid accessToken);
		bool IsActiveAccessToken(Guid accessToken);
	}
}