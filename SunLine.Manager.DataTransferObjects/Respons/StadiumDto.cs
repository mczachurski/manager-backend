using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.DataTransferObjects.Response
{
	public class StadiumDto : BaseEntityDto
	{
		public StadiumDto(Stadium stadium) : base(stadium)
		{
			Name = stadium.Name;
			Capacity = stadium.Capacity;
			TeamId = stadium.TeamId;
		}

		public int TeamId { get; set; }
		public string Name { get; set; }
		public int Capacity { get; set; }
	}
}