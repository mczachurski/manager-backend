namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class ResultDto
	{
		public bool IsSuccess { get ; set; }
		public string Message { get; set; }
		
		private ResultDto()
		{
			
		}
		
		public static ResultDto CreateError(string message)
		{
			return new ResultDto { IsSuccess = false, Message = message };
		}
		
		public static ResultDto CreateSuccess()
		{
			return new ResultDto { IsSuccess = true };
		}
	}
}