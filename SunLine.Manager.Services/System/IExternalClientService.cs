using System;

namespace SunLine.Manager.Services.System
{
	public interface IExternalClientService
	{
		bool IsClientKeyValid(Guid clientKey);
	}
}