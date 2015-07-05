using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Football;

namespace SunLine.Manager.Services.Football
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
		
		public void Update(Team team)
		{
			_teamRepository.Update(team);
		}
		
		public Team Create(Team team)
		{
			return _teamRepository.Create(team);
		}
	}
}