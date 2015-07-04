using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.DataTransferObjects.Response
{
	public class TeamDto : BaseEntityDto
	{		
		public TeamDto(Team team) : base(team)
		{
			Name = team.Name;
			UserId = team.UserId;
		}
		
		public string Name { get ; set; }		
		public int UserId { get; set; }
	}
}