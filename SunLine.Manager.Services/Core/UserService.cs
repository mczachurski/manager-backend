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
	}
}