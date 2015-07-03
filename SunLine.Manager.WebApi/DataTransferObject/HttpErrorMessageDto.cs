using System.Collections.Generic;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class HttpErrorMessageDto
	{
		public string Message { get; set; }
		public string Documentation { get; set; }
		public IEnumerable<ModelFieldErrorDto> ModelErrors { get; set; }
		
		private HttpErrorMessageDto()
		{
			
		}
		
		public static HttpErrorMessageDto Create(string message)
		{
			return Create(message, null, "unknown");
		}
		
		public static HttpErrorMessageDto Create(string message, string documentation)
		{
			return Create(message, null, documentation);
		}
		
		public static HttpErrorMessageDto Create(string message, IEnumerable<ModelFieldErrorDto>  modelErrors)
		{
			return Create(message, modelErrors, "unknown");
		}
		
		public static HttpErrorMessageDto Create(string message, IEnumerable<ModelFieldErrorDto>  modelErrors, string documentation)
		{
			return new HttpErrorMessageDto { Message = message, Documentation = documentation, ModelErrors = modelErrors };
		}
	}
}