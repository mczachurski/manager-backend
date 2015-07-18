namespace SunLine.Manager.Entities
{
	public class OperationResult
	{
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }
		public object Data { get; set; }
	}
}