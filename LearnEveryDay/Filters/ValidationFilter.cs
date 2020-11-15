using System.Linq;
using System.Threading.Tasks;
using LearnEveryDay.Contracts.v1.Responses;
using LearnEveryDay.Contracts.V1.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearnEveryDay.Filters
{
    /**
     * ValidationFilters is a Middleware which intercepts the request before it hits the Controller method.
     *
     * We validate the request object in the Controller method arguments with the Validation class found in /Validators
     *
     * Important to "return;" in case we find errors so we don't call next(); which will carry on to next Middleware or Controller method
     */
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (!context.ModelState.IsValid)
            {
                var modelStateErrors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(error => error.Key, error => error.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();

                var errorResponse = new ValidationErrorResponse();

                foreach (var errorDic in modelStateErrors)
                {
                    foreach (var error in errorDic.Value)
                    {
                        var errorModel = new ErrorModel
                        {
                            FieldName = errorDic.Key,
                            Message = error
                        };
                        
                        errorResponse.Errors.Add(errorModel);
                    }
                }
                
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}