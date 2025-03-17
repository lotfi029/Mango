using System.Net;

namespace Mango.Web.Abstracts;

public record Error(string Code, string Description, HttpStatusCode? StatusCode)
{
    public static Error None => new(string.Empty, string.Empty, null);

    public static implicit operator Result(Error error)
        => new(false, error);

    public static Error BadRequest(string code, string description)
        => new(code, description, HttpStatusCode.BadRequest);

    public static Error NotFound(string code, string description)
        => new(code, description, HttpStatusCode.NotFound);

    public static Error FromException<T>(T ex) where T : Exception
        => new(ex.GetType().Name, ex.Message, HttpStatusCode.InternalServerError);

    public static Error Confilct(string code, string description)
        => new(code, description, HttpStatusCode.Conflict);

    public static Error Forbidden(string code, string description)
        => new(code, description, HttpStatusCode.Forbidden);

    public static Error Unauthorized(string code, string description)
        => new(code, description, HttpStatusCode.Unauthorized);
}
