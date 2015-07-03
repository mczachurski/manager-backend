using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using SunLine.Manager.WebApi.DataTransferObject;

namespace SunLine.Manager.WebApi.HttpResult
{
    public static class HttpResultControllerExtension
    {
        [NonAction]
        public static HttpForbiddenObjectResult HttpForbidden(this Controller controller, string message, string documentation = null)
        {
            return new HttpForbiddenObjectResult(ErrorResponseDto.Create(message, documentation));
        }
        
        [NonAction]
        public static BadRequestObjectResult HttpBadRequest(this Controller controller, string message, string documentation = null)
        {
            return controller.HttpBadRequest(ErrorResponseDto.Create(message, documentation));
        }
        
        [NonAction]
        public static HttpNotFoundObjectResult HttpNotFound(this Controller controller, string message, string documentation = null)
        {
            return controller.HttpNotFound(ErrorResponseDto.Create(message, documentation));
        }
        
        [NonAction]
        public static BadRequestObjectResult HttpBadModelState(this Controller controller, string message, string documentation = null)
        {            
            IList<ModelFieldErrorDto> modelErrors = new List<ModelFieldErrorDto>();
            foreach(var item in controller.ModelState)
            {
                var errors = item.Value.Errors.Select(x => x.ErrorMessage);
                modelErrors.Add(new ModelFieldErrorDto(item.Key, errors));
            }
            
            return controller.HttpBadRequest(ErrorResponseDto.Create(message, modelErrors, documentation));
        }
    }
}