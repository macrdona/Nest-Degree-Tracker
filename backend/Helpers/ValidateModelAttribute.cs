using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

/*
 * Thes classes allow us to customize error response that an API Controller returns
 * when there are validation errors in the model state
 */
namespace backend.Helpers
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState) : base (new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    public class ValidationResultModel
    {
        public string StatusCode { get; }
        public string Message { get; }
        public List<ValidationError> Errors { get; } 
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            StatusCode = "400";
            Message = "One or more required fields are missing";
            Errors = modelState.Keys.SelectMany(key => modelState[key].Errors
                                    .Select(e => new ValidationError(key, e.ErrorMessage)))
                                    .ToList();
        }
    }

    public class ValidationError
    {
        public string? Field { get; }
        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
