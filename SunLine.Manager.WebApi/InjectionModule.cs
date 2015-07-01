using System.Linq;
using System.Reflection;
using Autofac;
using SunLine.Manager.Repositories.Core;
using SunLine.Manager.Services.Core;

namespace SunLine.Manager.WebApi
{
    public class InjectionModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterServices(builder);
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