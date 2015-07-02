using Microsoft.AspNet.Mvc;

namespace SunLine.Manager.WebApi.HttpResult
{
        public static class HttpResultControllerExtension
        {
                [NonAction]
                public static HttpForbiddenObjectResult HttpForbidden(this Controller controller, object error)
                {
                    return new HttpForbiddenObjectResult(error);
        	}
        }
}