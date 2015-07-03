namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class SuccessResponseDto
	{
		public bool IsSuccess { get { return true; } }
		public string Message { get; set; }
		
		private SuccessResponseDto()
		{
			
		}
		
		public static SuccessResponseDto Create()
		{
			return Create(null);
		}
		
		public static SuccessResponseDto Create(string message)
		{
			return new SuccessResponseDto { Message = message };
		}
	}
}