using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.Services.Football
{
	public interface ITeamService
	{
		Team FindById(int id);
		Team Create(Team team);
		void Update(Team team);
	}
}