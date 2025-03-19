namespace Mango.Services.AuthAPI.Contracts;

public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName
);
