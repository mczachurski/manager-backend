using Microsoft.Framework.DependencyInjection;
using SunLine.Manager.Repositories.Core;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Core;

namespace SunLine.Manager.WebApi.DependencyInjection
{
	public class EmbeddedModule
	{
		public void Load(IServiceCollection services)
		{
			LoadRepositoryInfrastructure(services);
			LoadRepositories(services);
			LoadServices(services);
		}
		
		private void LoadRepositoryInfrastructure(IServiceCollection services)
		{
			services.AddTransient<IUnitOfWork, UnitOfWork>();
		}
		
		private void LoadRepositories(IServiceCollection services)
		{
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
		}
		
		private void LoadServices(IServiceCollection services)
		{
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeamService, TeamService>();
		}
	}
}