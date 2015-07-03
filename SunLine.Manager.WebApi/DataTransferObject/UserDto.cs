using System.ComponentModel.DataAnnotations;
using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class UserDto : BaseEntityDto
	{
		public UserDto()
		{
		}
		
		public UserDto(User user) : base(user)
		{
			FirstName = user.FirstName;
			LastName = user.LastName;
			Email = user.Email;
		}
		
		[Required(ErrorMessage = "Firstname is required")]
		public string FirstName { get ; set; }
		
		[Required(ErrorMessage = "Lastname is required")]
		public string LastName { get ; set; }
		
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email has bad format")]
		public string Email { get ; set; }
		
		[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
		public string Password { get ; set; }
	}
}