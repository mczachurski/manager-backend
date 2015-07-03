using System.ComponentModel.DataAnnotations;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class UserDto
	{
		[Required(ErrorMessage = "Firstname is required")]
		public string FirstName { get ; set; }
		
		[Required(ErrorMessage = "Lastname is required")]
		public string LastName { get ; set; }
		
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email has bad format")]
		public string Email { get ; set; }
	}
}