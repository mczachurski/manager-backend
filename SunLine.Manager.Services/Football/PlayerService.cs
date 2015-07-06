using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Football;

namespace SunLine.Manager.Services.Football
{
	public class PlayerService : IPlayerService
	{
		private readonly IPlayerRepository _playerRepository;
		
		public PlayerService(IPlayerRepository playerRepository)
		{
			_playerRepository = playerRepository;
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
			for(int i = 0; i < 18; ++i)
			{
				CreatePlayerData(team);
			}
		}
		
		private void CreatePlayerData(Team team)
		{
			var player = new Player
			{
				FirstName = "John",
				LastName = "Smith",
				Team = team,
				TeamId = team.Id
			};
			
			_playerRepository.Create(player);			
		}
	}
}