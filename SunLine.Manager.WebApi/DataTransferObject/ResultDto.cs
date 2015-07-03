namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class ResultDto
	{
		public bool IsSuccess { get ; set; }
		public string Message { get; set; }
		public string Documentation { get; set; }
		
		private ResultDto()
		{
			
		}

		public static ResultDto CreateError(string message)
		{
			return CreateError(message, "unknown");
		}
		
		public static ResultDto CreateError(string message, string documentation)
		{
			return new ResultDto { IsSuccess = false, Message = message, Documentation = documentation };
		}
		
		public static ResultDto CreateSuccess()
		{
			return new ResultDto { IsSuccess = true };
		}
	}
}