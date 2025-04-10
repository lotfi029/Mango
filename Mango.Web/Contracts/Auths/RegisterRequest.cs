namespace Mango.Web.Contracts.Auths;

public record RegisterRequest(
    string Email,
    string UserName,
    string Password,
    string FirstName,
    string LastName
);
