using System.Collections.Generic;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class ErrorResponseDto
	{
		public bool IsSuccess { get { return false; } }
		public string Message { get; set; }
		public string Documentation { get; set; }
		public IEnumerable<ModelFieldErrorDto> ModelErrors { get; set; }
		
		private ErrorResponseDto()
		{
			
		}
		
		public static ErrorResponseDto Create(string message)
		{
			return Create(message, null, "unknown");
		}
		
		public static ErrorResponseDto Create(string message, string documentation)
		{
			return Create(message, null, documentation);
		}
		
		public static ErrorResponseDto Create(string message, IEnumerable<ModelFieldErrorDto>  modelErrors)
		{
			return Create(message, modelErrors, "unknown");
		}
		
		public static ErrorResponseDto Create(string message, IEnumerable<ModelFieldErrorDto>  modelErrors, string documentation)
		{
			return new ErrorResponseDto { Message = message, Documentation = documentation, ModelErrors = modelErrors };
		}
	}
}