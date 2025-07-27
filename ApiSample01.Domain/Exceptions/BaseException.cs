namespace ApiSample01.Domain.Exceptions;

public abstract class BaseException : Exception
{
    public int HttpCode { get; protected set; } = 0;
    public string HttpMessage { get; protected set; } = string.Empty;
    public bool Status { get; } = false;
    public string ErrorCode { get; protected set; } = string.Empty;
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