namespace Mango.Services.AuthAPI.Abstracts;

public record Error(string Code, string Description, int? StatusCode)
{
    public static Error None => new(string.Empty, string.Empty, null);

    public static implicit operator Result(Error error)
        => new(false, error);

    public static Error BadRequest(string code, string description)
        => new(code, description, 400);

    public static Error NotFound(string code, string description)
        => new(code, description, 404);

    public static Error InternalServerError(string code, string description)
        => new(code, description, 500);

    public static Error FromException<T>(T ex) where T : Exception
        => new(ex.GetType().Name, ex.Message, 500);

    public static Error Conflict(string code, string description)
        => new(code, description, 409);

    public static Error Forbidden(string code, string description)
        => new(code, description, 403);

    public static Error Unauthorized(string code, string description)
        => new(code, description, 401);

    public static Error Locked(string code, string description)
        => new(code, description, 423);

}
