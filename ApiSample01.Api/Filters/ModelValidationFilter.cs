using ApiSample01.Api.Extensions;
using ApiSample01.Domain.Exceptions;
using ApiSample01.Application.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiSample01.Api.Filters;

public class ModelValidationFilter : ActionFilterAttribute, IExceptionFilter
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var apiError = context.ModelState.ToApiErrorResponse();
            context.Result = new BadRequestObjectResult(apiError);
        }
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ET002FieldSizeError ex)
        {
            var apiError = ErrorResponseHelper.CreateApiErrorResponse(ex);
            context.Result = new ObjectResult(apiError) { StatusCode = apiError.HttpCode };
            context.ExceptionHandled = true;
        }
    }
}