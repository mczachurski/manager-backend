using System.ComponentModel.DataAnnotations;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class SignInDto
	{
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }	
	}
}