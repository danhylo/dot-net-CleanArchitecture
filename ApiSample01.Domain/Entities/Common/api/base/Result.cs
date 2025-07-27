namespace ApiSample01.Domain.Entities.Common.Api.Base;

public class Result<TSuccess, TError>
{
    public bool IsSuccess { get; private set; }
    public TSuccess? Success { get; private set; }
    public TError? Error { get; private set; }

    private Result(TSuccess success)
    {
        IsSuccess = true;
        Success = success;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result<TSuccess, TError> Ok(TSuccess success) => new(success);
    public static Result<TSuccess, TError> Fail(TError error) => new(error);
}

public static class Result
{
    public static Result<TSuccess, ApiErrorResponse> Ok<TSuccess>(TSuccess success) => 
        Result<TSuccess, ApiErrorResponse>.Ok(success);
    
    public static Result<TSuccess, ApiErrorResponse> Fail<TSuccess>(ApiErrorResponse error) => 
        Result<TSuccess, ApiErrorResponse>.Fail(error);
}