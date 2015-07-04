using System.ComponentModel.DataAnnotations;

namespace SunLine.Manager.DataTransferObjects.Request
{
	public class CreateUserDto
	{	
		[Required(ErrorMessage = "Firstname is required")]
		public string FirstName { get ; set; }
		
		[Required(ErrorMessage = "Lastname is required")]
		public string LastName { get ; set; }
		
		[Required(ErrorMessage = "TeamName is required")]
		public string TeamName { get ; set; }
		
		[Required(ErrorMessage = "StadiumName is required")]
		public string StadiumName { get ; set; }
		
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email has bad format")]
		public string Email { get ; set; }
		
		[Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
		public string Password { get ; set; }
	}
}