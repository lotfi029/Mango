namespace Mango.Web.Contracts.Auths;

public record LoginRequest(
    string Email,
    string Password
);
