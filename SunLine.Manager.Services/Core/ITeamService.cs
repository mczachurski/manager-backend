using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.Services.Core
{
	public interface ITeamService
	{
		Team FindById(int id);
		Team Create(Team team);
		void Update(Team team);
	}
}