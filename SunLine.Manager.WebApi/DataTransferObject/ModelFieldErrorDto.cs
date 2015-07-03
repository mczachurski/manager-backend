using System.Collections.Generic;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class ModelFieldErrorDto
	{
		public string FieldName { get; set; }
		public IEnumerable<string> Errors { get; set; }
		
		public ModelFieldErrorDto(string fieldName, IEnumerable<string> errors)
		{
			FieldName = fieldName;
			Errors = errors;
		}
	}
}