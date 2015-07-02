using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Core;

namespace SunLine.Manager.Services.Core
{
	public class TeamService : ITeamService
	{
		private readonly ITeamRepository _teamRepository;
		
		public TeamService(ITeamRepository teamRepository)
		{
			_teamRepository = teamRepository;
		}
		
		public Team FindById(int id)
		{
			return _teamRepository.FindById(id);
		}
		
		public Team Create(Team team)
		{
			return _teamRepository.Create(team);
		}
		
		public void Update(Team team)
		{
			_teamRepository.Update(team);
		}
	}
}