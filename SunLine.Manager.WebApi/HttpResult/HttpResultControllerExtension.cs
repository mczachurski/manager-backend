using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using SunLine.Manager.Entities;
using SunLine.Manager.DataTransferObjects.Response;

namespace SunLine.Manager.WebApi.HttpResult
{
    public static class HttpResultControllerExtension
    {
        [NonAction]
        public static HttpForbiddenObjectResult HttpForbidden(this Controller controller, string message, string documentation = null)
        {
            return new HttpForbiddenObjectResult(ErrorDto.Create(message, documentation));
        }
        
        [NonAction]
        public static BadRequestObjectResult HttpBadRequest(this Controller controller, string message, string documentation = null)
        {
            return controller.HttpBadRequest(ErrorDto.Create(message, documentation));
        }
        
        [NonAction]
        public static HttpNotFoundObjectResult HttpNotFound(this Controller controller, string message, string documentation = null)
        {
            return controller.HttpNotFound(ErrorDto.Create(message, documentation));
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
            
            return controller.HttpBadRequest(ErrorDto.Create(message, modelErrors, documentation));
        }
        
        [NonAction]
        public static BadRequestObjectResult HttpErrorDuringOperation(this Controller controller, string message, OperationResult operationResult, string documentation = null)
        {                        
            string errorMessage = operationResult.ErrorMessage != null ? operationResult.ErrorMessage : string.Empty;
            errorMessage = $"{message} {errorMessage}";
            return controller.HttpBadRequest(ErrorDto.Create(errorMessage, documentation));
        }
        
        [NonAction]
        public static BadRequestObjectResult HttpBadModelState(this Controller controller, string message, IList<ModelFieldErrorDto> modelErrors, string documentation = null)
        {                        
            return controller.HttpBadRequest(ErrorDto.Create(message, modelErrors, documentation));
        }
    }
}