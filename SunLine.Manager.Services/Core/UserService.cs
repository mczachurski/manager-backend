using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Core;
using SunLine.Manager.Repositories.Football;
using SunLine.Manager.Services.Football;
using SunLine.Manager.DataTransferObjects.Request;

namespace SunLine.Manager.Services.Core
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly ITeamService _teamService;
		private readonly IStadiumRepository _stadiumRepository;
		
		public UserService(IUserRepository userRepository, ITeamService teamService, IStadiumRepository stadiumRepository)
		{
			_userRepository = userRepository;
			_teamService = teamService;
			_stadiumRepository = stadiumRepository;
		}
		
		public User FindById(int id)
		{
			return _userRepository.FindById(id);
		}
		
		public User FindByCredentials(string email, string password)
		{
			User user = _userRepository.FindByEmail(email);
			if(user == null)
			{
				return null;
			}
			
			if(!PasswordHash.ValidatePassword(password, user.Password))
			{
				return null;
			}
			
			return user;
		}
		
		public User Create(CreateUserDto createUserDto)
		{
            var user = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
				Password = CalculatePasswordHash(createUserDto.Password)
            };
			user = _userRepository.Create(user);
			
            var team = new Team
            {
                Name = createUserDto.TeamName,
                User = user,
                UserId = user.Id
            };
			team = _teamService.Create(team);
             
            var stadium = new Stadium
            {
                Name = createUserDto.StadiumName,
                Capacity = 5000,
				Team = team,
				TeamId = team.Id
            };			
			
			stadium = _stadiumRepository.Create(stadium);
			
			return user;
		}
		
		public User FindByEmail(string email)
		{
			return _userRepository.FindByEmail(email);
		}
		
		private string CalculatePasswordHash(string password)
		{
			var passwordHash = PasswordHash.CreateHash(password);
			return passwordHash;
		}
	}
}