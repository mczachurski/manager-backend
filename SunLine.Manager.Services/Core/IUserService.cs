using SunLine.Manager.Entities.Core;
using SunLine.Manager.DataTransferObjects.Request;

namespace SunLine.Manager.Services.Core
{
	public interface IUserService
	{
		User FindById(int id);
		User Create(CreateUserDto createUserDto);
		User FindByEmail(string email);
		User FindByCredentials(string email, string password);
	}
}