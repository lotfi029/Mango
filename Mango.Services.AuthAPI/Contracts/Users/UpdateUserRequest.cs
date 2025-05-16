namespace Store.Services.AuthAPI.Contracts.Users;
public record UpdateUserRequest(
    string FirstName,
    string LastName
    );