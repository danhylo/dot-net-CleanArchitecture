namespace ApiSample01.Application.Common.Helpers;

using ApiSample01.Domain.Entities.Common.Api.Base;
using ApiSample01.Domain.Exceptions;
using ApiSample01.Domain.Constants;

public static class ErrorResponseHelper
{


    public static ApiErrorResponse CreateApiErrorResponse(Exception exception)
    {
        if (exception is BaseException baseException)
        {
            return new ApiErrorResponse
            {
                HttpCode = baseException.HttpCode,
                HttpMessage = baseException.HttpMessage,
                Status = baseException.Status,
                Error = new Error
                {
                    Code = baseException.ErrorCode,
                    Message = baseException.ErrorMessage,
                    Application = baseException.Application
                },
                Transaction = TransactionHelper.CreateTransaction()
            };
        }

        return CreateApiErrorResponse(500, "Internal Server Error", "EP:999", "Erro Desconhecido", ApplicationConstants.WEATHER_API_NAME);
    }
    public static ApiErrorResponse CreateApiErrorResponse(int httpCode, string httpMessage, string errorCode, string errorMessage, string application) => new()
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