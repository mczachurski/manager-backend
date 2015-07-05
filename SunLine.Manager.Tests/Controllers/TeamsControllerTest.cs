using Microsoft.AspNet.Mvc;
using Xunit;
using Moq;
using SunLine.Manager.WebApi.Controllers;
using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Football;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Football;

namespace SunLine.Manager.Tests.Controllers
{
	public class TeamsControllerTest
	{
		Mock<ITeamService> _teamService;
		TeamsController _teamsController;
		
		private void CreateTeamsControllerMocks()
		{
			_teamService = new Mock<ITeamService>();
			_teamsController = new TeamsController(_teamService.Object);
		}
		
		[Fact]
		public void GetTeamMustReturnNotFoundWhenTeamNotExists()
		{
			CreateTeamsControllerMocks();
			
			var actionResult = _teamsController.Get(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Team (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetTeamMustReturnTeamDataWhenTeamExists()
		{
			CreateTeamsControllerMocks();
			_teamService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new Team() { Id = id, Name = "FC Barcelona" });
			
			var actionResult = _teamsController.Get(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(1, data.Id);
			Assert.Equal("FC Barcelona", data.Name);   
		}
	}
}