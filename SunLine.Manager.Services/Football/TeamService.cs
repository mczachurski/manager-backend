using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Football;

namespace SunLine.Manager.Services.Football
{
	public class TeamService : ITeamService
	{
		private readonly ITeamRepository _teamRepository;
		private readonly IPlayerService _playerService;
		
		public TeamService(ITeamRepository teamRepository, IPlayerService playerService)
		{
			_teamRepository = teamRepository;
			_playerService = playerService;
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
			team = _teamRepository.Create(team);
			_playerService.GeneratePlayerData(team);
			
			return team;
		}
	}
}