using System;
using SunLine.Manager.Repositories.Core; 

namespace SunLine.Manager.Services.Core
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