namespace SunLine.Manager.DataTransferObjects.Response
{
	public class SuccessDto
	{
		public bool IsSuccess { get { return true; } }
		public string Message { get; set; }
		
		private SuccessDto()
		{
			
		}
		
		public static SuccessDto Create()
		{
			return Create(null);
		}
		
		public static SuccessDto Create(string message)
		{
			return new SuccessDto { Message = message };
		}
	}
}