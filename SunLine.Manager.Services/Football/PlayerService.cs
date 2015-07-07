using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Football;

namespace SunLine.Manager.Services.Football
{
	public class PlayerService : IPlayerService
	{
		private readonly IPlayerRepository _playerRepository;
		private readonly ITeamRepository _teamRepository;
		
		public PlayerService(IPlayerRepository playerRepository, ITeamRepository teamRepository)
		{
			_playerRepository = playerRepository;
			_teamRepository = teamRepository;
		}
		
		public Player FindById(int id)
		{
			return _playerRepository.FindById(id);
		}
		
		public void Update(Player player)
		{
			_playerRepository.Update(player);
		}
		
		public Player Create(Player player)
		{
			return _playerRepository.Create(player);
		}
		
		public void GeneratePlayerData(Team team)
		{
			var playerPositions = _teamRepository.GetPlayerPositionsForTeamSetup(team.TeamSetup);
			for(int i = 0; i < playerPositions.Count; ++i)
			{
				CreatePlayerData(team, playerPositions[i]);
			}
			
			int restOfPlayers = 18 - playerPositions.Count;
			for(int i = 0; i < restOfPlayers; ++i)
			{
				CreatePlayerData(team, PlayerPositionEnum.Unknown);
			} 
		}
		
		private void CreatePlayerData(Team team, PlayerPositionEnum playerPosition)
		{
			if(playerPosition == PlayerPositionEnum.Unknown)
			{
				// TODO: Generate position.
				playerPosition = PlayerPositionEnum.Goalkeeper;		
			}
			
			var player = new Player
			{
				FirstName = "John",
				LastName = "Smith",
				Team = team,
				TeamId = team.Id,
				PlayerPosition = playerPosition
			};
			
			_playerRepository.Create(player);			
		}
	}
}