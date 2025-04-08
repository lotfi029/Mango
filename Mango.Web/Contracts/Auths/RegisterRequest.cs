namespace Mango.Web.Contracts.Auths;

public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName
);
