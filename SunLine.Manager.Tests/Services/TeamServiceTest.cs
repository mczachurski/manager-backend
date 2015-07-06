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
	public class TeamServiceTest : DependencyInjectedBaseTest
	{
		[Fact]
		public void TeamHaveToHaveProperAmountOfPlayerAfterCreating()
		{
			var _teamService = _container.Resolve<ITeamService>();
			var team = new Team
			{
				Name = "FC Barcelona",
                User = new User { Id = 1 },
                UserId = 1
			};
			
			team = _teamService.Create(team);
			
			Assert.Equal(18, team.Players.Count);
		}
		
		[Fact]
		public void AfterCreatingTeamNewPlayersMustBeSavedInDatabase()
		{
			var _teamService = _container.Resolve<ITeamService>();
			var _unitOfWork = _container.Resolve<IUnitOfWork>();
			var team = new Team
			{
				Name = "FC Barcelona",
                User = new User { Id = 1 },
                UserId = 1
			};
			
			team = _teamService.Create(team);
			_unitOfWork.Commit();
			
			var databaseContext = _container.Resolve<DatabaseContext>();
			var amountOfPlayers = databaseContext.Players.Count(x => x.TeamId == team.Id);
			Assert.Equal(18, amountOfPlayers);
		}
	}
}