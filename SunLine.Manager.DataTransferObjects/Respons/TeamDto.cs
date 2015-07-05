using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.DataTransferObjects.Response
{
	public class TeamDto : BaseEntityDto
	{		
		public TeamDto(Team team) : base(team)
		{
			Name = team.Name;
			UserId = team.UserId;
			StadiumId = team.StadiumId;
		}
		
		public string Name { get ; set; }		
		public int UserId { get; set; }
		public int StadiumId { get; set; }
	}
}