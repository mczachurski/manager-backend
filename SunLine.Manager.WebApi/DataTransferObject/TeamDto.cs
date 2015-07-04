using System.ComponentModel.DataAnnotations;
using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class TeamDto : BaseEntityDto
	{
		public TeamDto()
		{
			
		}
		
		public TeamDto(Team team) : base(team)
		{
			Name = team.Name;
			UserId = team.UserId;
		}
		
		[Required(ErrorMessage = "Team name is required")]
		public string Name { get ; set; }
		
		public int UserId { get; set; }
	}
}