namespace Store.Services.AuthAPI.Contracts.Users;

public record UserResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    DateTime CreateAt
    );