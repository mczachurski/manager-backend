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
			
			team.Attack += player.Attack;
			team.Defense += player.Defense;
			team.Pass += player.Pass;
			
			player.Price = CalculatePrice(player);
			_playerRepository.Create(player);			
		}
		
		private double CalculatePrice(Player player)
		{
			double price = 0.0;
			int playerRate = player.GetPlayerRate();
			
			if(playerRate < 20)
			{
				price = 100000.0;	
			}
			else if(playerRate >= 20 && playerRate < 25)
			{
				price = 300000.0;
			}
			else if(playerRate >= 25 && playerRate < 30)
			{
				price = 800000.0;
			}
			else if(playerRate >= 30 && playerRate < 35)
			{
				price = 1300000.0;
			}
			else if(playerRate >= 35 && playerRate < 40)
			{
				price = 1800000.0;
			}
			else if(playerRate >= 40 && playerRate < 45)
			{
				price = 2400000.0;
			}
			else if(playerRate >= 45 && playerRate < 50)
			{
				price = 2400000.0;
			}
			else if(playerRate >= 50 && playerRate < 55)
			{
				price = 3200000.0;
			}
			else if(playerRate >= 55 && playerRate < 60)
			{
				price = 4000000.0;
			}
			else if(playerRate >= 60 && playerRate < 65)
			{
				price = 6000000.0;
			}
			else if(playerRate >= 65 && playerRate < 70)
			{
				price = 9000000.0;
			}
			else if(playerRate >= 70 && playerRate < 75)
			{
				price = 13000000.0;
			}
			else if(playerRate >= 75 && playerRate < 80)
			{
				price = 20000000.0;
			}
			else if(playerRate >= 80 && playerRate < 85)
			{
				price = 30000000.0;
			}
			else if(playerRate >= 85 && playerRate < 90)
			{
				price = 45000000.0;
			}
			else if(playerRate >= 90 && playerRate < 95)
			{
				price = 70000000.0;
			}
			else if(playerRate >= 95 && playerRate < 100)
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