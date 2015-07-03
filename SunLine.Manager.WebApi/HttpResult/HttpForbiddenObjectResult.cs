using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;

namespace SunLine.Manager.WebApi.HttpResult
{
    public class HttpForbiddenObjectResult : ObjectResult
    {
        public HttpForbiddenObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}