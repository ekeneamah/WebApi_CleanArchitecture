using Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Filters;

public class ValidatorActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
          
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(new ApiResult<object>
            {
                Data = null, Message = string.Join(Environment.NewLine, GetErrorListFromModelState(context.ModelState))
            });
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //Do nothing
    }

    private static IEnumerable<string> GetErrorListFromModelState
        (ModelStateDictionary modelState)
    {
        var query = from state in modelState.Values
            from error in state.Errors
            select error.ErrorMessage;

        var errorList = query.ToList();
        return errorList;
    }
}
