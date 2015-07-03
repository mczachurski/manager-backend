using System;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.System;

namespace SunLine.Manager.Services.System
{
	public interface IUserSessionService
	{
        UserSession CreateUserSession(User user, Guid accessToken, string host);
        void AbortUserSession(Guid accessToken);
        UserSession FindByAccessToken(Guid accessToken);
		bool IsActiveAccessToken(Guid accessToken);
	}
}