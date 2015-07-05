using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using SunLine.Manager.Services.Core;
using SunLine.Manager.DataTransferObjects.Response;

namespace SunLine.Manager.WebApi.Attributes
{
	public class CheckClientKeyAttribute : ActionFilterAttribute
	{
		private readonly IExternalClientService _externalClientService;
		
		public CheckClientKeyAttribute(IExternalClientService externalClientService)
		{
			_externalClientService = externalClientService;
		}
		
	    public override void OnActionExecuting(ActionExecutingContext actionContext)
	    {		
			var clientKeyString = actionContext.HttpContext.Request.Headers["ClientKey"];
			
	        if (string.IsNullOrWhiteSpace(clientKeyString))
	        {
				var objectResult = new ObjectResult(ErrorDto.Create("ClientKey is not provided", DocumentationLinks.ClientKey));
				objectResult.StatusCode = StatusCodes.Status403Forbidden;
	            actionContext.Result = objectResult;
				return;
	        }
			
			Guid clientKey = Guid.Empty; 
			if(!Guid.TryParse(clientKeyString, out clientKey))
			{
				var objectResult = new ObjectResult(ErrorDto.Create("ClientKey have bad format", DocumentationLinks.ClientKey));
				objectResult.StatusCode = StatusCodes.Status403Forbidden;
	            actionContext.Result = objectResult;
				return;
			}
			
			if(!_externalClientService.IsClientKeyValid(clientKey))
			{
				var objectResult = new ObjectResult(ErrorDto.Create("ClientKey is not authorized", DocumentationLinks.ClientKey));
				objectResult.StatusCode = StatusCodes.Status403Forbidden;
	            actionContext.Result = objectResult;
			}
	    }
	}
}