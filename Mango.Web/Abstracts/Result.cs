namespace Mango.Web.Abstracts;

public class Result
{
    public Result(bool isSucceed, Error error)
    {
        if (isSucceed && error != Error.None || !isSucceed && error == Error.None)
            throw new ArgumentException("Result is succeed, but error is not None.", nameof(error));

        IsSucceed = isSucceed;
        Error = error;
    }
    public bool IsSucceed { get; set; }
    public Error Error { get; set; }

    public static Result Success()
        => new(true, Error.None);

    public static Result Fail(Error error)
        => new(false, error);
    
    public static Result<T> Success<T>(T value)
        => new(true, Error.None, value);

    public static Result<T> Fail<T>(Error error)
        => new(false, error, default!);
}

public class Result<T>(bool IsSucceed, Error Error, T Value) : Result(IsSucceed, Error)
{
    private T _value = Value;

    public T Value
    {
        get
        {
            return IsSucceed
                ? _value
                : throw new ArgumentException("invalid getting data");
        }
        set { _value = value; }
    }
}
