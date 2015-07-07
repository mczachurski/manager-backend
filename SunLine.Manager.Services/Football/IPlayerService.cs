using System.Collections.Generic;
using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.Services.Football
{
	public interface IPlayerService
	{
		Player FindById(int id);
		IList<Player> FindAllPlayersForTeam(int teamId);
		void Update(Player player);
		Player Create(Player player);
		void GeneratePlayerData(Team team);
	}
}