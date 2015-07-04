namespace SunLine.Manager.Entities.Football
{
	public class Stadium : BaseEntity
	{
		public Team Team { get; set; }
		public int TeamId { get; set; }
		public string Name { get; set; }
		public int Capacity { get; set; }
	}
}