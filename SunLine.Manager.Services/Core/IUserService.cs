using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.Services.Core
{
	public interface IUserService
	{
		User FindById(int id);
		User Create(User user, string password);
		User FindByEmail(string email);
		User FindByCredentials(string email, string password);
	}
}