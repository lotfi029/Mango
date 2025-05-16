namespace Store.Abstractions.Abstraction;
public record Error(string Description, int? Code)
{
    public static Error NoN => new("NoN", null);
    public static Error NotFound(string description) => new(description, 404);
    public static Error BadRequest(string description) => new(description, 400);
    public static Error Locked(string description) => new(description, 423);
    public static Error Conflict(string description) => new(description, 409);
    public static Error Unauthorized(string description) => new(description, 401);
    public static Error Forbidden(string description) => new(description, 403);
    public static Error InternalServerError(string description) => new(description, 500);

    public static implicit operator Result(Error error)
        => new(false, error);
}
