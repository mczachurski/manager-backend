using System.Collections.Generic;

namespace SunLine.Manager.DataTransferObjects.Response
{
	public class ErrorDto
	{
		public bool IsSuccess { get { return false; } }
		public string Message { get; set; }
		public string Documentation { get; set; }
		public IEnumerable<ModelFieldErrorDto> ModelErrors { get; set; }
		
		private ErrorDto()
		{
			
		}
		
		public static ErrorDto Create(string message)
		{
			return Create(message, null, "unknown");
		}
		
		public static ErrorDto Create(string message, string documentation)
		{
			return Create(message, null, documentation);
		}
		
		public static ErrorDto Create(string message, IEnumerable<ModelFieldErrorDto>  modelErrors)
		{
			return Create(message, modelErrors, "unknown");
		}
		
		public static ErrorDto Create(string message, IEnumerable<ModelFieldErrorDto>  modelErrors, string documentation)
		{
			return new ErrorDto { Message = message, Documentation = documentation, ModelErrors = modelErrors };
		}
	}
}