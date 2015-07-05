using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.Services.Football
{
	public interface ITeamService
	{
		Team FindById(int id);
		void Update(Team team);
		Team Create(Team team);
	}
}