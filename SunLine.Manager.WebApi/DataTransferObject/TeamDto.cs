using System.ComponentModel.DataAnnotations;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class TeamDto
	{
		[Required(ErrorMessage = "Team name is required")]
		public string Name { get ; set; }
		public string UserId { get; set; }
	}
}