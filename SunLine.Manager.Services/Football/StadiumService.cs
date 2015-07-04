using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Football;

namespace SunLine.Manager.Services.Football
{
	public class StadiumService : IStadiumService
	{
		private readonly IStadiumRepository _stadiumRepository;
		
		public StadiumService(IStadiumRepository stadiumRepository)
		{
			_stadiumRepository = stadiumRepository;
		}
		
		public Stadium FindById(int id)
		{
			return _stadiumRepository.FindById(id);
		}
		
		public void Update(Stadium stadium)
		{
			_stadiumRepository.Update(stadium);
		}
	}
}