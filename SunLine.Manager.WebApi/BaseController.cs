using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.AspNet.Mvc.ModelBinding;
using SunLine.Manager.DataTransferObjects.Response;

namespace SunLine.Manager.WebApi
{
    public class BaseController : Controller
    {
        protected readonly IModelMetadataProvider _modelMetadataProvider;
        protected readonly IObjectModelValidator _objectValidator;

        protected BaseController()
        {
        }

        protected BaseController(IModelMetadataProvider modelMetadataProvider, IObjectModelValidator objectValidator)
        {
            _modelMetadataProvider = modelMetadataProvider;
            _objectValidator = objectValidator;
            
            ValidationErrors = new List<ModelFieldErrorDto>();
        }

        public ApplicationContext ApplicationContext { get; set; }
        
        protected IList<ModelFieldErrorDto> ValidationErrors { get; private set; }

        protected bool IsValid(object model)
        {
            if(_modelMetadataProvider == null || _objectValidator == null)
            {
                throw new ValidationNotInitializedException("Validation is not initialized in Controller. You must execute diffrent constructor.");
            }
            
            var modelExplorer = _modelMetadataProvider.GetModelExplorerForType(model.GetType(), model);
            var modelValidationProvider = new DataAnnotationsModelValidatorProvider();

            var modelValidationContext = new ModelValidationContext(
            	null,
            	modelValidationProvider,
            	new ModelStateDictionary(),
            	modelExplorer
            );

            var excludedValidationTypesPredicate = new List<IExcludeTypeValidationFilter>();
            var topLevelValidationNode = new ModelValidationNode(string.Empty, modelExplorer.Metadata, model)
            {
                ValidateAllProperties = true
            };

            _objectValidator.Validate(modelValidationContext, topLevelValidationNode);

            ValidationErrors.Clear();
            foreach (var item in modelValidationContext.ModelState)
            {
                var errors = item.Value.Errors.Select(x => x.ErrorMessage);
                ValidationErrors.Add(new ModelFieldErrorDto(item.Key, errors));
            }
			            
			return ValidationErrors.Count == 0;
        }
    }
}