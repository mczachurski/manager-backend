using System;
using SunLine.Manager.Repositories.System; 

namespace SunLine.Manager.Services.System
{
	public class ExternalClientService : IExternalClientService
	{
		private readonly IExternalClientRepository _externalClientRepository;
		public ExternalClientService(IExternalClientRepository externalClientRepository)
		{
			_externalClientRepository = externalClientRepository;
		}
		
		public bool IsClientKeyValid(Guid clientKey)
		{
			return _externalClientRepository.IsClientKeyValid(clientKey);
		}
	}
}