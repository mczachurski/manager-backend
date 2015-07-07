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