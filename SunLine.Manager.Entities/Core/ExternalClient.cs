using  System;

namespace SunLine.Manager.Entities.Core
{
	public class ExternalClient : BaseEntity
	{
		public string ClientName { get; set; }
		
		public Guid ClientKey { get; set; }
	}
}