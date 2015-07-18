using System;
using System.Linq;
using Xunit;
using Moq;
using Autofac;
using SunLine.Manager.DataTransferObjects.Request;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Core;

namespace SunLine.Manager.Tests.Services
{
	public class UserServiceTest : DependencyInjectedBaseTest
	{		
		[Fact]
		public void UserWithCorrectDataMustBeSavedInDatabase()
		{
			var userService = _container.Resolve<IUserService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			
			var createUserDto = CreateCorrectUserDto();
			createUserDto.Email = "testing@fakeemail.com";
			userService.Create(createUserDto);
			unitOfWork.Commit();
			
			var databaseContext = _container.Resolve<DatabaseContext>();
			var user = databaseContext.Users.FirstOrDefault(x => x.Email == "testing@fakeemail.com");
			Assert.NotNull(user);
		}
		
		[Fact]
		public void TeamWithCorrectDataMustBeSavedInDatabaseDuringCreatingUser()
		{
			var userService = _container.Resolve<IUserService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			
			var createUserDto = CreateCorrectUserDto();
			createUserDto.TeamName = "Fake Team Name";
			userService.Create(createUserDto);
			unitOfWork.Commit();
			
			var databaseContext = _container.Resolve<DatabaseContext>();
			var team = databaseContext.Teams.FirstOrDefault(x => x.Name == "Fake Team Name");
			Assert.NotNull(team);
		}
		
		[Fact]
		public void StadiumWithCorrectDataMustBeSavedInDatabaseDuringCreatingUser()
		{
			var userService = _container.Resolve<IUserService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			
			var createUserDto = CreateCorrectUserDto();
			createUserDto.StadiumName = "Fake Stadium";
			userService.Create(createUserDto);
			unitOfWork.Commit();
			
			var databaseContext = _container.Resolve<DatabaseContext>();
			var stadium = databaseContext.Stadiums.FirstOrDefault(x => x.Name == "Fake Stadium");
			Assert.NotNull(stadium);
		}
		
		[Fact]
		public void NewStadiumMustHaveProperCapacity()
		{
			var userService = _container.Resolve<IUserService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			
			var createUserDto = CreateCorrectUserDto();
			createUserDto.StadiumName = "Stadium Capacity";
			userService.Create(createUserDto);
			unitOfWork.Commit();
			
			var databaseContext = _container.Resolve<DatabaseContext>();
			var stadium = databaseContext.Stadiums.FirstOrDefault(x => x.Name == "Stadium Capacity");
			Assert.NotNull(stadium);
			Assert.Equal(5000, stadium.Capacity);
		}
		
		[Fact]
		public void ServiceShouldFindUserByHisCredentials()
		{
			var userService = _container.Resolve<IUserService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			var createUserDto = CreateCorrectUserDto();
			createUserDto.Email = "password@fakeemail.com";
			createUserDto.Password = "fakepass";
			userService.Create(createUserDto);
			unitOfWork.Commit();
			
			var findedUser = userService.FindByCredentials("password@fakeemail.com", "fakepass");
			
			Assert.NotNull(findedUser);
			Assert.Equal("password@fakeemail.com", findedUser.Email);
		}
		
		[Fact]
		public void SystemShouldStorePlainUserPasswordsInDatabase()
		{
			var userService = _container.Resolve<IUserService>();
			var unitOfWork = _container.Resolve<IUnitOfWork>();
			
			var createUserDto = CreateCorrectUserDto();
			createUserDto.Email = "encrypted@fakeemail.com";
			createUserDto.Password = "superpassword";
			userService.Create(createUserDto);
			unitOfWork.Commit();
			
			var databaseContext = _container.Resolve<DatabaseContext>();
			var user = databaseContext.Users.FirstOrDefault(x => x.Email == "encrypted@fakeemail.com");
			Assert.NotNull(user);
			Assert.NotEqual("superpassword", user.Password);
		}
		
		private CreateUserDto CreateCorrectUserDto()
		{
			var createUserDto = new CreateUserDto
			{
				FirstName = "John",
				LastName  = "Smith",
				TeamName = "FC Barcelona",
				StadiumName = "Great Stadium",
				Email = "test@email.com",
				Password = "password",
				TeamSetup = "Setup442A"
			};
			
			return createUserDto;
		}
	}
}