using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;

namespace SunLine.Manager.WebApi.HttpResult
{
	/// <summary>
    /// An <see cref="ObjectResult"/> that when executed will produce a Forbidden (403) response.
    /// </summary>
    public class HttpForbiddenObjectResult : ObjectResult
    {
        /// <summary>
        /// Creates a new <see cref="HttpForbiddenObjectResult"/> instance.
        /// </summary>
        /// <param name="value">The value to format in the entity body.</param>
        public HttpForbiddenObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}