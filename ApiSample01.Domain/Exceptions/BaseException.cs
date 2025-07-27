namespace ApiSample01.Domain.Exceptions;

public abstract class BaseException : Exception
{
    public int HttpCode { get; } = 400;
    public string HttpMessage { get; } = "Bad Request";
    public bool Status { get; } = false;
    public string ErrorCode { get; } = "ET:002";
    public string ErrorMessage { get; protected set; } = string.Empty;
    public string Application { get; protected set; } = string.Empty;

    protected BaseException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    protected BaseException(string message, Exception innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}