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
	public class PlayersControllerTest
	{
		Mock<IPlayerService> _playerService;
		PlayersController _playersController;
		
		private void CreateTeamsControllerMocks()
		{
			_playerService = new Mock<IPlayerService>();
			_playersController = new PlayersController(_playerService.Object);
		}
		
		[Fact]
		public void GetPlayerMustReturnNotFoundWhenPlayerNotExists()
		{
			CreateTeamsControllerMocks();
			
			var actionResult = _playersController.Get(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Player (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetPlayerMustReturnPlayerDataWhenTeamExists()
		{
			CreateTeamsControllerMocks();
			_playerService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new Player() { Id = id, FirstName = "Robert" });
			
			var actionResult = _playersController.Get(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(1, data.Id);
			Assert.Equal("Robert", data.FirstName);   
		}
	}
}