using System;

namespace SunLine.Manager.Services.Core
{
	public interface IExternalClientService
	{
		bool IsClientKeyValid(Guid clientKey);
	}
}