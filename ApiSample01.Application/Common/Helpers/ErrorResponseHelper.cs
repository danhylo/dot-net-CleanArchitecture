namespace ApiSample01.Application.Common.Helpers;

using ApiSample01.Domain.Entities.Common.Api.Base;

public static class ErrorResponseHelper
{
    public static ApiErrorResponse CreateErrorResponse(int httpCode, string httpMessage, string errorCode, string errorMessage, string application, Exception ex) => new()
    {
        HttpCode = httpCode,
        HttpMessage = httpMessage,
        Status = false,
        Error = new Error
        {
            Code = errorCode,
            Message = errorMessage,
            Application = application
        },
        Transaction = TransactionHelper.CreateTransaction()
    };
}