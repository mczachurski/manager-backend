namespace SunLine.Manager.WebApi
{
	public class ApplicationContext
	{
		public ApplicationContext(int userId)
		{
			UserId = userId;
		}
		
		public int UserId { get; }
	}
}