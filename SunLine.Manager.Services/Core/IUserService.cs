using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.Services.Core
{
	public interface IUserService
	{
		User FindById(int id);
	}
}