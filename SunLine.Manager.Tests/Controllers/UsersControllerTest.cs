using System;
using Microsoft.AspNet.Mvc;
using Xunit;
using Moq;
using SunLine.Manager.WebApi.Controllers;
using SunLine.Manager.DataTransferObjects.Request;
using SunLine.Manager.DataTransferObjects.Response;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Core;
using SunLine.Manager.Repositories.Football;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Services.Football;

namespace SunLine.Manager.Tests.Controllers
{
	public class UsersControllerTest
	{
		private Mock<IUnitOfWork> _unitOfWork;
		private Mock<IUserService> _userService;
		private Mock<ITeamService> _teamService;
		private Mock<IUserSessionService> _userSessionService;
		private Mock<IStadiumService> _stadiumService;
		private UsersController _usersController;
		
		private void CreateUserControllerMocks()
		{			
			_unitOfWork = new Mock<IUnitOfWork>();
			_userService = new Mock<IUserService>();
			_teamService = new Mock<ITeamService>();
			_userSessionService = new Mock<IUserSessionService>();
			_stadiumService = new Mock<IStadiumService>();
			_usersController = new UsersController(_unitOfWork.Object, _userService.Object, _teamService.Object, 
				_userSessionService.Object, _stadiumService.Object);
		}
		
		[Fact]
		public void SignInMustReturnBadRequestWhenSignInDataIsEmpty()
		{
			CreateUserControllerMocks();
			
			var actionResult = _usersController.SignIn(null) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(400, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Sign in data not specified", data.Message);
		}
		
		[Fact]
		public void SignInMustReturnBadRequestWhenSignInModelStateIsInvalid()
		{
			CreateUserControllerMocks();
			var signInDto = new SignInDto { Email = string.Empty, Password = "password" };
			_usersController.ModelState.AddModelError("Email", "Email is required");
			
			var actionResult = _usersController.SignIn(signInDto) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(400, actionResult.StatusCode);
			Assert.NotNull(actionResult.Value);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Error in sign in data model", data.Message);
			Assert.Equal("Email", data.ModelErrors[0].FieldName);  
		}

		[Fact]
		public void SignInMustReturnFailureMessageAfterNotSuccessfullSignIn()
		{
			CreateUserControllerMocks();
			var signInDto = new SignInDto { Email = "email@email.com", Password = "password" };
			
			var actionResult = _usersController.SignIn(signInDto) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
			Assert.Equal("Email or password are incorrect", data.Message);
			Assert.Equal(false, data.IsSuccess);
		}
		
		[Fact]
		public void SignInMustReturnAccessTokenAfterSuccessfullSignIn()
		{
			CreateUserControllerMocks();
			var user = new User { Id = 1, FirstName = "John", LastName = "Smith" };
			var userSession = new UserSession { AccessToken = Guid.Parse("09d7ab22-a936-4ac9-8fb8-4ba7dc95815e"), User = user, IsActive = true };
			_userService.Setup(x => x.FindByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns((string email, string password) => user);
			_userSessionService.Setup(x => x.CreateUserSession(It.IsAny<User>(), It.IsAny<string>())).Returns((User userData, string host) => userSession);	
			var signInDto = new SignInDto { Email = "email@email.com", Password = "password" };
			
			var actionResult = _usersController.SignIn(signInDto) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
			Assert.Equal("09d7ab22-a936-4ac9-8fb8-4ba7dc95815e", data.AccessToken);
			Assert.Equal(true, data.IsSuccess);
		}
		
		[Fact]
		public void GetUserMustReturnNotFoundWhenUserNotExists()
		{
			CreateUserControllerMocks();
			
			var actionResult = _usersController.GetUser(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserMustReturnUserDataWhenUserExists()
		{
			CreateUserControllerMocks();
			_userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new User() { Id = id, FirstName = "John", LastName = "Smith" });
			
			var actionResult = _usersController.GetUser(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(1, data.Id);
			Assert.Equal("John", data.FirstName);
			Assert.Equal("Smith", data.LastName);   
		}
		
		[Fact]
		public void GetUserTeamMustReturnNotFoundWhenUserNotExists()
		{
			CreateUserControllerMocks();
			
			var actionResult = _usersController.GetUserTeam(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserTeamMustReturnNotFoundWhenUserTeamNotExists()
		{
			CreateUserControllerMocks();
			_userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new User() { Id = id, FirstName = "John", LastName = "Smith" });
			
			var actionResult = _usersController.GetUserTeam(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Team for user (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserTeamMustReturnTeamWhenUserTeamExists()
		{
			CreateUserControllerMocks();
			_userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => 
				new User() { Id = id, FirstName = "John", LastName = "Smith", TeamId = 2 }
			);
			_teamService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) =>
				new Team { Id = id, Name = "FC Barcelona" }
			);
			
			var actionResult = _usersController.GetUserTeam(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(2, data.Id);
			Assert.Equal("FC Barcelona", data.Name);   
		}
		
		[Fact]
		public void GetUserStadiumMustReturnNotFoundWhenUserNotExists()
		{
			CreateUserControllerMocks();
			
			var actionResult = _usersController.GetUserStadium(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserStadiumMustReturnNotFoundWhenUserTeamNotExists()
		{
			CreateUserControllerMocks();
			_userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new User() { Id = id, FirstName = "John", LastName = "Smith" });
			
			var actionResult = _usersController.GetUserStadium(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Team for user (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserStadiumMustReturnNotFoundWhenUserStadiumNotExists()
		{
			CreateUserControllerMocks();
			_userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new User() { Id = id, FirstName = "John", LastName = "Smith", TeamId = 1 });
			_teamService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => new Team() { Id = id, Name = "FC Barcelona", StadiumId = 0 });
			
			var actionResult = _usersController.GetUserStadium(1) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(404, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("Stadium for user (1) not exists", data.Message);
		}
		
		[Fact]
		public void GetUserTeamMustReturnStadiumWhenUserStadiumExists()
		{
			CreateUserControllerMocks();
			_userService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => 
				new User() { Id = id, FirstName = "John", LastName = "Smith", TeamId = 2 }
			);
			_teamService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) =>
				new Team { Id = id, Name = "FC Barcelona", StadiumId = 3 }
			);
			_stadiumService.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) => 
				new Stadium() { Id = id, Name = "Super Stadium" }
			);
			
			var actionResult = _usersController.GetUserStadium(1) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(3, data.Id);
			Assert.Equal("Super Stadium", data.Name);   
		}
		
		[Fact]
		public void CreateUserMustReturnBadRequestWhenUserDataIsEmpty()
		{
			CreateUserControllerMocks();
			
			var actionResult = _usersController.CreateUser(null) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(400, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User data not specified", data.Message);
		}
		
		[Fact]
		public void CreateUserMustReturnBadRequestWhenUserModelStateIsInvalid()
		{
			CreateUserControllerMocks();
			var createUserDto = new CreateUserDto { FirstName = "John", LastName = "Smith", Email = "bademailformat" };
			_usersController.ModelState.AddModelError("Email", "Bad email format");
			
			var actionResult = _usersController.CreateUser(createUserDto) as ObjectResult;
			
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
			CreateUserControllerMocks();
			var createUserDto = new CreateUserDto { FirstName = "John", LastName = "Smith", Email = "johnsmith@email.com" };
			_userService.Setup(x => x.FindByEmail(It.IsAny<string>())).Returns((string email) => 
				new User() { Id = 1, FirstName = "John", LastName = "Smith", TeamId = 1, Email = email }
			);
			
			var actionResult = _usersController.CreateUser(createUserDto) as ObjectResult;
			
			Assert.NotNull(actionResult);
			Assert.Equal(403, actionResult.StatusCode);
			dynamic data = actionResult.Value;  
    		Assert.Equal("User with this email already exists", data.Message); 
		}
		
		[Fact]
		public void CreateUserMustReturnSuccessWhenUserIsValid()
		{
			CreateUserControllerMocks();
			var createUserDto = new CreateUserDto { FirstName = "John", LastName = "Smith", Email = "email@email.com" };
			
			var actionResult = _usersController.CreateUser(createUserDto) as JsonResult;
			
			Assert.NotNull(actionResult);
			dynamic data = actionResult.Value;  
    		Assert.Equal(true, data.IsSuccess);   
		}
	}
}