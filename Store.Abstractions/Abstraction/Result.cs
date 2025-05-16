namespace Store.Abstractions.Abstraction;
public class Result
{
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.NoN || !isSuccess && error == Error.NoN)
            throw new ArgumentException("Invalid combination of isSuccess and error");

        IsSuccess = isSuccess;
        Error = error;
        IsFailure = !isSuccess;
    }
    public bool IsSuccess { get; }
    public bool IsFailure { get; }
    public Error Error { get; }

    public static Result Success() => new(true, Error.NoN);
    public static Result Failure(Error error) => new(false, error);

    public static Result<T> Success<T>(T data) => new(true, Error.NoN, data);
    public static Result<T> Failure<T>(Error error) => new(false, error, default!);
}

public class Result<T>(bool isSuccess, Error error, T data) : Result(isSuccess, error)
{
    private readonly T _data = data;
    public T Data
    {
        get
        {
            if (IsFailure)
                throw new InvalidOperationException("Cannot access data when result is a failure");
            return _data;
        }
    }
    public static implicit operator Result<T>(Error error)
    => new(false, error, default!);
    public static implicit operator Result<T>(T value)
        => new(true, Error.NoN, value);
}
