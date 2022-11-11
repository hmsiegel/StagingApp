namespace StagingApp.Domain.Shared;
public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result Fail(Error error) => new(false, error);
    public static Result<TValue> Failuer<TValue>(Error error) => new(default, true, error);
    public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failuer<TValue>(Error.NullValue);


}
