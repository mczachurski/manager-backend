using Microsoft.Framework.DependencyInjection;
using SunLine.Manager.Repositories.Core;
using SunLine.Manager.Repositories.Football;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Services.Football;
using SunLine.Manager.WebApi.Attributes;

namespace SunLine.Manager.WebApi.DependencyInjection
{
	public class EmbeddedModule
	{
		public void Load(IServiceCollection services)
		{
			LoadActionFilters(services);
			LoadRepositoryInfrastructure(services);
			LoadRepositories(services);
			LoadServices(services);
		}
		
		private void LoadActionFilters(IServiceCollection services)
		{
			services.AddTransient<CheckAccessTokenAttribute>();
			services.AddTransient<CheckClientKeyAttribute>();
		}
		
		private void LoadRepositoryInfrastructure(IServiceCollection services)
		{
			services.AddTransient<IUnitOfWork, UnitOfWork>();
		}
		
		private void LoadRepositories(IServiceCollection services)
		{
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
			services.AddTransient<IStadiumRepository, StadiumRepository>();
			services.AddTransient<IPlayerRepository, PlayerRepository>();
			services.AddTransient<IUserSessionRepository, UserSessionRepository>();
			services.AddTransient<IExternalClientRepository, ExternalClientRepository>();
		}
		
		private void LoadServices(IServiceCollection services)
		{
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeamService, TeamService>();
			services.AddTransient<IStadiumService, StadiumService>();
			services.AddTransient<IUserSessionService, UserSessionService>();
			services.AddTransient<IExternalClientService, ExternalClientService>();
		}
	}
}