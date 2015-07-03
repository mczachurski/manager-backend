using Microsoft.AspNet.Mvc;
using Xunit;
using Moq;
using SunLine.Manager.WebApi.Controllers;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Core;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Core;

namespace SunLine.Manager.Tests.Controllers
{
	public class UsersControllerTest
	{
		[Fact]
		public void GetUserMustReturnNotFoundWhenUserNotExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			
			var actionResult = usersController.GetUser(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
		}
		
		[Fact]
		public void GetUserMustReturnUserDataWhenUserExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var teamService = new Mock<ITeamService>();
			var userService = new Mock<IUserService>();
			userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new User() { Id = id, FirstName = "John", LastName = "Smith" });
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			
			var actionResult = usersController.GetUser(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(1, data.Id);
			Assert.Equal("John", data.FirstName);
			Assert.Equal("Smith", data.LastName);   
		}
	}
}