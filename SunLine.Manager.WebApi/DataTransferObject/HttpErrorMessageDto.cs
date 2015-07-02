namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class HttpErrorMessageDto
	{
		public string Message { get; set; }
		public string Documentation { get; set; }
		
		private HttpErrorMessageDto()
		{
			
		}
		
		public static HttpErrorMessageDto Create(string message)
		{
			return Create(message, "unknown");
		}
		
		public static HttpErrorMessageDto Create(string message, string documentation)
		{
			return new HttpErrorMessageDto { Message = message, Documentation = documentation };
		}
	}
}