using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.Services.Football
{
	public interface IPlayerService
	{
		Player FindById(int id);
		void Update(Player player);
		Player Create(Player player);
		void GeneratePlayerData(Team team);
	}
}