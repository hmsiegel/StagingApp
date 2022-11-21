namespace StagingApp.Domain.Exceptions;
public sealed class ProcessException : Exception
{
    public int ErrorCode { get; private set; }
    public string? ExceptionMessage { get; private set; } = string.Empty;

    public ProcessException(int errorCode, string? exceptionMessage)
    {
        ErrorCode = errorCode;
        ExceptionMessage = exceptionMessage;
    }

    public override string Message
    {
        get
        {
            if (Message is null)
            {
                throw new NullReferenceException();
            }

            return ExceptionMessage!;
        }
    }
}
