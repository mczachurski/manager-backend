using Microsoft.AspNet.Mvc;
using Xunit;
using Moq;
using SunLine.Manager.WebApi.Controllers;
using SunLine.Manager.WebApi.DataTransferObject;
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
			dynamic data = actionResult.Value;  
    		Assert.Equal("User (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserMustReturnUserDataWhenUserExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var teamService = new Mock<ITeamService>();
			var userService = new Mock<IUserService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new User() { Id = id, FirstName = "John", LastName = "Smith" });
			
			var actionResult = usersController.GetUser(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(1, data.Id);
			Assert.Equal("John", data.FirstName);
			Assert.Equal("Smith", data.LastName);   
		}
		
		[Fact]
		public void GetUserTeamMustReturnNotFoundWhenUserNotExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			
			var actionResult = usersController.GetUserTeam(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserTeamMustReturnNotFoundWhenUserTeamNotExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var teamService = new Mock<ITeamService>();
			var userService = new Mock<IUserService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new User() { Id = id, FirstName = "John", LastName = "Smith" });
			
			var actionResult = usersController.GetUserTeam(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Team for user (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserTeamMustReturnTeamWhenUserTeamExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var teamService = new Mock<ITeamService>();
			var userService = new Mock<IUserService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => 
				new User() { Id = id, FirstName = "John", LastName = "Smith", TeamId = 1 }
			);
			teamService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) =>
				new Team { Id = id, Name = "FC Barcelona" }
			);
			
			var actionResult = usersController.GetUserTeam(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(1, data.Id);
			Assert.Equal("FC Barcelona", data.Name);   
		}
		
		[Fact]
		public void CreateUserMustReturnBadRequestWhenUserDataIsEmpty()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			
			var actionResult = usersController.CreateUser(null) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(400, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User data not specified", data.Message);
		}
		
		[Fact]
		public void CreateUserMustReturnBadRequestWhenUserModelStateIsInvalid()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			var userDto = new UserDto { FirstName = "John", LastName = "Smith", Email = "bademailformat" };
			usersController.ModelState.AddModelError("Email", "Bad email format");
			
			var actionResult = usersController.CreateUser(userDto) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(400, actionResult.StatusCode);
			Assert.NotNull(actionResult.Value);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Error in user data model", data.Message);
			Assert.Equal("Email", data.ModelErrors[0].FieldName);  
		}
		
		[Fact]
		public void CreateUserMustReturnForbiddenWhenUserWithThisSameEmailExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			var userDto = new UserDto { FirstName = "John", LastName = "Smith", Email = "johnsmith@email.com" };
			userService.Setup(x => x.FindByEmail(It.IsAny<string>())).Returns((string email) => 
				new User() { Id = 1, FirstName = "John", LastName = "Smith", TeamId = 1, Email = email }
			);
			
			var actionResult = usersController.CreateUser(userDto) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(403, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User with this email already exists", data.Message); 
		}
		
		[Fact]
		public void CreateUserMustReturnSuccessWhenUserIsValid()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var teamService = new Mock<ITeamService>();
			var userService = new Mock<IUserService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			var userDto = new UserDto { FirstName = "John", LastName = "Smith", Email = "email@email.com" };
			
			var actionResult = usersController.CreateUser(userDto) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(true, data.IsSuccess);   
		}
		
		[Fact]
		public void CreateTeamMustReturnBadRequestWhenTeamDataIsEmpty()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			
			var actionResult = usersController.CreateTeam(1, null) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(400, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Team data not specified", data.Message);
		}
		
		[Fact]
		public void CreateTeamMustReturnNotFoundWhenUserNotExists()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			var teamDto = new TeamDto();
			
			var actionResult = usersController.CreateTeam(0, teamDto) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User (0) not exists", data.Message);
		}
		
		[Fact]
		public void CreateTeamMustReturnForbiddenWhenUserAlreadyHasTeam()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => 
				new User() { Id = id, FirstName = "John", LastName = "Smith", TeamId = 1 }
			);
			var teamDto = new TeamDto();
			
			var actionResult = usersController.CreateTeam(1, teamDto) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(403, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User already have a team", data.Message);
		}
		
		[Fact]
		public void CreateTeamMustReturnBadRequestWhenTeamModelStateIsInvalid()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => 
				new User() { Id = id, FirstName = "John", LastName = "Smith", TeamId = 0 }
			);
			var teamDto = new TeamDto();
			usersController.ModelState.AddModelError("Name", string.Empty);
			
			var actionResult = usersController.CreateTeam(1, teamDto) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(400, actionResult.StatusCode);
			Assert.NotNull(actionResult.Value);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Error in team data model", data.Message);
			Assert.Equal("Name", data.ModelErrors[0].FieldName);  
		}
		
		[Fact]
		public void CreateTeamMustReturnSuccessWhenTeamIsValid()
		{
			var unitOfWork = new Mock<IUnitOfWork>();
			var userService = new Mock<IUserService>();
			var teamService = new Mock<ITeamService>();
			var usersController = new UsersController(unitOfWork.Object, userService.Object, teamService.Object);
			userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => 
				new User() { Id = id, FirstName = "John", LastName = "Smith", TeamId = 0 }
			);
			var teamDto = new TeamDto { Name = "FC Barcelona" };
			
			var actionResult = usersController.CreateTeam(1, teamDto) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(true, data.IsSuccess);
		}
	}
}