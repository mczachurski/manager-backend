using System.Linq;
using Xunit;
using Moq;
using Autofac;
using SunLine.Manager.Tests;
using SunLine.Manager.DataTransferObjects.Request;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Services.Football;

namespace SunLine.Manager.Tests
{
	public class PlayerServiceTest : DependencyInjectedBaseTest
	{
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor442ATeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 12;
			team.TeamSetup = TeamSetupEnum.Setup442A;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor442BTeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 13;
			team.TeamSetup = TeamSetupEnum.Setup442B;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.OffensiveMidfield ) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.DefensiveMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor433ATeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 14;
			team.TeamSetup = TeamSetupEnum.Setup433A;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor433BTeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 15;
			team.TeamSetup = TeamSetupEnum.Setup433B;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.DefensiveMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor451ATeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 16;
			team.TeamSetup = TeamSetupEnum.Setup451A;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor451BTeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup451B;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.OffensiveMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.DefensiveMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor532ATeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup532A;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor532BTeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup532B;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.OffensiveMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.DefensiveMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor541ATeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup541A;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor541BTeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup541B;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.OffensiveMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.DefensiveMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor343ATeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup343A;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor343BTeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup343B;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.OffensiveMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.DefensiveMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor4231ATeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup4231A;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.OffensiveMidfield) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.DefensiveMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor4231BTeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup4231B;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.DefensiveMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor352ATeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup352A;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreMidfield) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightMidfield) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor352BTeamSettings()
		{
			var playerService = _container.Resolve<IPlayerService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = CreateCorrectTeam();
			team.Id = 17;
			team.TeamSetup = TeamSetupEnum.Setup352B;
			
			playerService.GeneratePlayerData(team);
			unitOfWork.Commit();
			
			var players = playerService.FindAllPlayersForTeam(team.Id);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreForward) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.OffensiveMidfield) >= 3);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.DefensiveMidfield) >= 2);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.CentreBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.LeftBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.RightBack) >= 1);
			Assert.True(players.Count(x => x.PlayerPosition == PlayerPositionEnum.Goalkeeper) >= 1);
		}
		
		private Team CreateCorrectTeam()
		{
			var team = new Team
			{
				Name = "FC Barcelona",
                User = new User { Id = 1 },
                UserId = 1,
				TeamSetup = TeamSetupEnum.Setup442A
			};
			
			return team;
		}
	}
}	