namespace Mango.Services.ProductAPI.Abstracts;

public record Result
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
        => new(value, true, Error.None);
    public static Result<T> Fail<T>(Error error)
        => new(default!, false, error);
}

public record Result<T>(T Value, bool IsSucceed, Error Error) : Result(IsSucceed, Error)
{
    public static implicit operator Result<T>(Error error)
        => new(default!, false, error);

    private T _value = Value;

    public T Value
    {
        get => IsSucceed ? _value : throw new InvalidOperationException("Result is not succeed.");
        init => _value = value;
    }

}