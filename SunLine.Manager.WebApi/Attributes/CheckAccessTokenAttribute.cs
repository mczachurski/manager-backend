using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using SunLine.Manager.Services.System;
using SunLine.Manager.WebApi.DataTransferObject;

namespace SunLine.Manager.WebApi.Attributes
{
	public class CheckAccessTokenAttribute : ActionFilterAttribute
	{
		private readonly IUserSessionService _userSessionService;
		
		public CheckAccessTokenAttribute(IUserSessionService userSessionService)
		{
			_userSessionService = userSessionService;
		}
		
	    public override void OnActionExecuting(ActionExecutingContext actionContext)
	    {			
			var accessTokenString = actionContext.HttpContext.Request.Headers["AccessToken"];
			
	        if (string.IsNullOrWhiteSpace(accessTokenString))
	        {
				var objectResult = new ObjectResult(HttpErrorMessageDto.Create("AccessToken is not provided", DocumentationLinks.AccessToken));
				objectResult.StatusCode = StatusCodes.Status401Unauthorized;
	            actionContext.Result = objectResult;
				return;
	        }
			
			Guid accessToken = Guid.Empty; 
			if(!Guid.TryParse(accessTokenString, out accessToken))
			{
				var objectResult = new ObjectResult(HttpErrorMessageDto.Create("AccessToken have bad format", DocumentationLinks.ClientKey));
				objectResult.StatusCode = StatusCodes.Status401Unauthorized;
	            actionContext.Result = objectResult;
				return;
			}
			
			if(!_userSessionService.IsActiveAccessToken(accessToken))
			{
				var objectResult = new ObjectResult(HttpErrorMessageDto.Create("AccessToken is not authorized", DocumentationLinks.ClientKey));
				objectResult.StatusCode = StatusCodes.Status401Unauthorized;
	            actionContext.Result = objectResult;
			}
	    }
	}
}