using SunLine.Manager.Entities.Dict;

namespace SunLine.Manager.Entities.Football
{
	public class Referee : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Country Country { get; set; }
	}
}