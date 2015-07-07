using System;
using System.Linq;
using System.Collections.Generic;
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
		
		public IList<Player> FindAllPlayersForTeam(int teamId)
		{
			return _playerRepository.FindAll().Where(x => x.TeamId == teamId).ToList();
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
			
			CreatePlayerData(team, PlayerPositionEnum.Goalkeeper);
			
			int restOfPlayers = 17 - playerPositions.Count;
			for(int i = 0; i < restOfPlayers; ++i)
			{
				CreatePlayerData(team, PlayerPositionEnum.Unknown);
			}
		}
		
		private void CreatePlayerData(Team team, PlayerPositionEnum playerPosition)
		{
			if(playerPosition == PlayerPositionEnum.Unknown)
			{
				Random random = new Random();
				int randomPosition = random.Next(1, 12);
				playerPosition = (PlayerPositionEnum) randomPosition;		
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