using Autofac;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.WebApi.DependencyInjection;

namespace SunLine.Manager.Tests
{
	public class DependencyInjectedBaseTest
	{
		public IContainer _container;
		
		public DependencyInjectedBaseTest()
		{
			var builder = new ContainerBuilder();
			builder.RegisterInstance(new DatabaseContext()).As<DatabaseContext>();
			
			var autofacModule = new AutofacModule();
			autofacModule.RegisterAllTypes(builder);
			
			_container = builder.Build();
		}
	}
}