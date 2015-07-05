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
	public class StadiumsControllerTest
	{
		Mock<IStadiumService> _stadiumService;
		StadiumsController _stadiumsController;
		
		private void CreateStadiumsControllerMocks()
		{			
			_stadiumService = new Mock<IStadiumService>();
			_stadiumsController = new StadiumsController(_stadiumService.Object);
		}
		
		[Fact]
		public void GetStadiumMustReturnNotFoundWhenStadiumNotExists()
		{
			CreateStadiumsControllerMocks();
			
			var actionResult = _stadiumsController.Get(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Stadium (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetStadiumMustReturnStadiumDataWhenStadiumExists()
		{
			CreateStadiumsControllerMocks();
			_stadiumService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new Stadium() { Id = id, Name = "FC Barcelona" });
			
			var actionResult = _stadiumsController.Get(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(1, data.Id);
			Assert.Equal("FC Barcelona", data.Name);   
		}
	}
}