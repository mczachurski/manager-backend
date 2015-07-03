using SunLine.Manager.Entities.Core;
using SunLine.Manager.Repositories.Core;

namespace SunLine.Manager.Services.Core
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
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
		
		public User Create(User user, string password)
		{
			CalculatePasswordHash(user, password);
			return _userRepository.Create(user);
		}
		
		public User FindByEmail(string email)
		{
			return _userRepository.FindByEmail(email);
		}
		
		private void CalculatePasswordHash(User user, string password)
		{
			var passwordHash = PasswordHash.CreateHash(password);
			user.Password = passwordHash;
		}
	}
}