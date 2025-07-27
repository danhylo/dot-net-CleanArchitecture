using ApiSample01.Application.Common.Api.Base;
using ApiSample01.Application.Common.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiSample01.Api.Extensions;

public static class ModelStateExtensions
{
    public static ApiErrorResponse ToApiErrorResponse(this ModelStateDictionary modelState)
    {
        var firstError = modelState
            .Where(x => x.Value?.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors.Select(e => new { Field = x.Key, Message = e.ErrorMessage }))
            .FirstOrDefault();

        return new ApiErrorResponse
        {
            HttpCode = 400,
            HttpMessage = "Bad Request",
            Status = false,
            Error = new Error
            {
                Code = "VALIDATION_ERROR",
                Message = firstError?.Message ?? "Validation failed",
                Application = "ApiSample01"
            },
            Transaction = new Transaction
            {
                LocalTransactionId = Guid.NewGuid().ToString(),
                LocalTransactionDate = DateTime.UtcNow
            }
        };
    }
}