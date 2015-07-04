using Microsoft.AspNet.Mvc;
using Xunit;
using Moq;
using SunLine.Manager.WebApi.Controllers;
using SunLine.Manager.WebApi.DataTransferObject;
using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Football;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Football;

namespace SunLine.Manager.Tests.Controllers
{
	public class TeamsControllerTest
	{
		[Fact]
		public void GetTeamMustReturnNotFoundWhenTeamNotExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var teamService = new Mock<ITeamService>();
			var teamsController = new TeamsController(unitOfWork.Object, teamService.Object);
			
			var actionResult = teamsController.Get(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Team (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserMustReturnTeamDataWhenTeamExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var teamService = new Mock<ITeamService>();
			var teamsController = new TeamsController(unitOfWork.Object, teamService.Object);
			teamService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new Team() { Id = id, Name = "FC Barcelona" });
			
			var actionResult = teamsController.Get(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(1, data.Id);
			Assert.Equal("FC Barcelona", data.Name);   
		}
	}
}