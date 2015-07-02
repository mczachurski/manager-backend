using System.ComponentModel.DataAnnotations;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class UserDto
	{
		[Required(ErrorMessage = "Team name is required")]
		public string FirstName { get ; set; }
		
		[Required(ErrorMessage = "Team name is required")]
		public string LastName { get ; set; }
		
		[Required(ErrorMessage = "Team name is required")]
		[EmailAddress(ErrorMessage = "Email has bad format")]
		public string Email { get ; set; }
	}
}