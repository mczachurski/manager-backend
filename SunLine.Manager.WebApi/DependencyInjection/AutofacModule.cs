using System.Linq;
using System.Reflection;
using Autofac;
using SunLine.Manager.Repositories.Core;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Core;
using SunLine.Manager.WebApi.Attributes;

namespace SunLine.Manager.WebApi.DependencyInjection
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterAllTypes(builder);
        }
        
        public void RegisterAllTypes(ContainerBuilder builder)
        {
            RegisterActionFilters(builder);
            RegisterRepositoryInfrastructure(builder);
            RegisterRepositories(builder);
            RegisterServices(builder);
        }
        
        private void RegisterActionFilters(ContainerBuilder builder)
		{
			builder.RegisterType<CheckAccessTokenAttribute>();
            builder.RegisterType<CheckClientKeyAttribute>();
		}
		
		private void RegisterRepositoryInfrastructure(ContainerBuilder builder)
		{
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
		}

        private void RegisterRepositories(ContainerBuilder builder)
        {
            var repositoryAssembly = typeof(UserRepository).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(repositoryAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            var serviceAssembly = typeof(UserService).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(serviceAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
