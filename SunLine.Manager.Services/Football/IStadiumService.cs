using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.Services.Football
{
	public interface IStadiumService
	{
		Stadium FindById(int id);
		void Update(Stadium stadium);
	}
}