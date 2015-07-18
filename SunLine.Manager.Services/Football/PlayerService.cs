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
			Random random = new Random();
			
			if(playerPosition == PlayerPositionEnum.Unknown)
			{
				int randomPosition = random.Next(1, 12);
				playerPosition = (PlayerPositionEnum) randomPosition;		
			}
			
			int randomFoot = random.Next(1, 2);
			FavouriteFootEnum favouriteFoot = (FavouriteFootEnum) randomFoot; 
		
			var player = new Player
			{
				FirstName = "John",
				LastName = "Smith",
				Team = team,
				TeamId = team.Id,
				PlayerPosition = playerPosition,
				FavouriteFoot = favouriteFoot,
				Attack = GenerateAttackPoints(playerPosition),
				Pass = GeneratePassPoints(playerPosition),
				Defense = GenerateDefensePoints(playerPosition),
				Age = random.Next(17, 23),
				Height = random.Next(150, 200),
				Weight = random.Next(60, 100)
			};
			
			player.Price = CalculatePrice(player);
			_playerRepository.Create(player);			
		}
		
		private double CalculatePrice(Player player)
		{
			double price = 0.0;
			
			if(player.PlayerRate < 20)
			{
				price = 100000.0;	
			}
			else if(player.PlayerRate >= 20 && player.PlayerRate < 25)
			{
				price = 300000.0;
			}
			else if(player.PlayerRate >= 25 && player.PlayerRate < 30)
			{
				price = 800000.0;
			}
			else if(player.PlayerRate >= 30 && player.PlayerRate < 35)
			{
				price = 1300000.0;
			}
			else if(player.PlayerRate >= 35 && player.PlayerRate < 40)
			{
				price = 1800000.0;
			}
			else if(player.PlayerRate >= 40 && player.PlayerRate < 45)
			{
				price = 2400000.0;
			}
			else if(player.PlayerRate >= 45 && player.PlayerRate < 50)
			{
				price = 2400000.0;
			}
			else if(player.PlayerRate >= 50 && player.PlayerRate < 55)
			{
				price = 3200000.0;
			}
			else if(player.PlayerRate >= 55 && player.PlayerRate < 60)
			{
				price = 4000000.0;
			}
			else if(player.PlayerRate >= 60 && player.PlayerRate < 65)
			{
				price = 6000000.0;
			}
			else if(player.PlayerRate >= 65 && player.PlayerRate < 70)
			{
				price = 9000000.0;
			}
			else if(player.PlayerRate >= 70 && player.PlayerRate < 75)
			{
				price = 13000000.0;
			}
			else if(player.PlayerRate >= 75 && player.PlayerRate < 80)
			{
				price = 20000000.0;
			}
			else if(player.PlayerRate >= 80 && player.PlayerRate < 85)
			{
				price = 30000000.0;
			}
			else if(player.PlayerRate >= 85 && player.PlayerRate < 90)
			{
				price = 45000000.0;
			}
			else if(player.PlayerRate >= 90 && player.PlayerRate < 95)
			{
				price = 70000000.0;
			}
			else if(player.PlayerRate >= 95 && player.PlayerRate < 100)
			{
				price = 100000000.0;
			}
			
			return price;
		}
		
		private int GenerateAttackPoints(PlayerPositionEnum playerPosition)
		{
			int attackPoints = 0;
			
			switch(playerPosition)
			{
				case PlayerPositionEnum.LeftForward:
				case PlayerPositionEnum.CentreForward:
				case PlayerPositionEnum.RightForward:
					attackPoints = GeneratePointsWhenPlayerIsGoodOnPosition();
					break;
				case PlayerPositionEnum.LeftMidfield:
				case PlayerPositionEnum.CentreMidfield:
				case PlayerPositionEnum.RightMidfield:
					attackPoints = GeneratePointsWhenPlayerIsMediumOnPosition();
					break;
				case PlayerPositionEnum.LeftBack:
				case PlayerPositionEnum.CentreBack:
				case PlayerPositionEnum.RightBack:
				case PlayerPositionEnum.Goalkeeper:
					attackPoints = GeneratePointsWhenPlayerIsWeakOnPosition();
					break;
				default:
					throw new NotImplementedException("It's impossible to generate attack points for this position.");
			}
			
			return attackPoints;
		}
		
		private int GeneratePassPoints(PlayerPositionEnum playerPosition)
		{
			int attackPoints = 0;
			
			switch(playerPosition)
			{
				case PlayerPositionEnum.LeftForward:
				case PlayerPositionEnum.CentreForward:
				case PlayerPositionEnum.RightForward:
					attackPoints = GeneratePointsWhenPlayerIsMediumOnPosition();
					break;
				case PlayerPositionEnum.LeftMidfield:
				case PlayerPositionEnum.CentreMidfield:
				case PlayerPositionEnum.RightMidfield:
					attackPoints = GeneratePointsWhenPlayerIsGoodOnPosition();
					break;
				case PlayerPositionEnum.LeftBack:
				case PlayerPositionEnum.CentreBack:
				case PlayerPositionEnum.RightBack:
					attackPoints = GeneratePointsWhenPlayerIsMediumOnPosition();
					break;
				case PlayerPositionEnum.Goalkeeper:
					attackPoints = GeneratePointsWhenPlayerIsWeakOnPosition();
					break;
				default:
					throw new NotImplementedException("It's impossible to generate pass points for this position.");
			}
			
			return attackPoints;
		}
		
		private int GenerateDefensePoints(PlayerPositionEnum playerPosition)
		{
			int attackPoints = 0;
			
			switch(playerPosition)
			{
				case PlayerPositionEnum.LeftForward:
				case PlayerPositionEnum.CentreForward:
				case PlayerPositionEnum.RightForward:
					attackPoints = GeneratePointsWhenPlayerIsWeakOnPosition();
					break;
				case PlayerPositionEnum.LeftMidfield:
				case PlayerPositionEnum.CentreMidfield:
				case PlayerPositionEnum.RightMidfield:
					attackPoints = GeneratePointsWhenPlayerIsMediumOnPosition();
					break;
				case PlayerPositionEnum.LeftBack:
				case PlayerPositionEnum.CentreBack:
				case PlayerPositionEnum.RightBack:
					attackPoints = GeneratePointsWhenPlayerIsGoodOnPosition();
					break;
				case PlayerPositionEnum.Goalkeeper:
					attackPoints = GeneratePointsWhenPlayerIsVeryGoodOnPosition();
					break;
				default:
					throw new NotImplementedException("It's impossible to generate defense points for this position.");
			}
			
			return attackPoints;
		}

		private int GeneratePointsWhenPlayerIsVeryGoodOnPosition()
		{
			Random random = new Random();
			int points = random.Next(30, 40);
			return points;
		}
		
		private int GeneratePointsWhenPlayerIsGoodOnPosition()
		{
			Random random = new Random();
			int points = random.Next(20, 30);
			return points;
		}
		
		private int GeneratePointsWhenPlayerIsMediumOnPosition()
		{
			Random random = new Random();
			int points = random.Next(10, 20);
			return points;
		}
		
		private int GeneratePointsWhenPlayerIsWeakOnPosition()
		{
			Random random = new Random();
			int points = random.Next(1, 10);
			return points;
		}
	}
}