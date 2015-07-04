using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.DataTransferObjects.Response
{
	public class UserDto : BaseEntityDto
	{	
		public UserDto(User user) : base(user)
		{
			FirstName = user.FirstName;
			LastName = user.LastName;
			Email = user.Email;
		}
		
		public string FirstName { get ; set; }
		public string LastName { get ; set; }
		public string Email { get ; set; }
	}
}