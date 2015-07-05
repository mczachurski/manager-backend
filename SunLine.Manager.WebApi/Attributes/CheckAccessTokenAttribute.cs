using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using SunLine.Manager.Services.Core;
using SunLine.Manager.DataTransferObjects.Response;

namespace SunLine.Manager.WebApi.Attributes
{
	public class CheckAccessTokenAttribute : ActionFilterAttribute
	{
		private readonly IUserSessionService _userSessionService;
		private readonly IUserService _userService;
		
		public CheckAccessTokenAttribute(IUserSessionService userSessionService, IUserService userService)
		{
			_userSessionService = userSessionService;
			_userService = userService;
		}
		
	    public override void OnActionExecuting(ActionExecutingContext actionContext)
	    {
			var accessTokenString = actionContext.HttpContext.Request.Headers["AccessToken"];
			
	        if (string.IsNullOrWhiteSpace(accessTokenString))
	        {
				var objectResult = new ObjectResult(ErrorDto.Create("AccessToken is not provided", DocumentationLinks.AccessToken));
				objectResult.StatusCode = StatusCodes.Status401Unauthorized;
	            actionContext.Result = objectResult;
				return;
	        }
			
			Guid accessToken = Guid.Empty; 
			if(!Guid.TryParse(accessTokenString, out accessToken))
			{
				var objectResult = new ObjectResult(ErrorDto.Create("AccessToken have bad format", DocumentationLinks.ClientKey));
				objectResult.StatusCode = StatusCodes.Status401Unauthorized;
	            actionContext.Result = objectResult;
				return;
			}
			
			var userSession = _userSessionService.FindByAccessToken(accessToken);
			if(userSession == null || !userSession.IsActive)
			{
				var objectResult = new ObjectResult(ErrorDto.Create("AccessToken is not authorized", DocumentationLinks.ClientKey));
				objectResult.StatusCode = StatusCodes.Status401Unauthorized;
	            actionContext.Result = objectResult;
				return;
			}
			
			var user = _userService.FindById(userSession.UserId);
			if (user == null)
			{
				var objectResult = new ObjectResult(ErrorDto.Create("User not exists", DocumentationLinks.ClientKey));
				objectResult.StatusCode = StatusCodes.Status401Unauthorized;
	            actionContext.Result = objectResult;
				return;
			}
			
			var controller = actionContext.Controller as BaseController;
			if (controller != null)
			{
				controller.ApplicationContext = new ApplicationContext(user.Id);
			}
	    }
	}
}