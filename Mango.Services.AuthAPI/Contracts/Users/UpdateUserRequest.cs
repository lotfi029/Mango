using FluentValidation;

namespace Mango.Services.AuthAPI.Contracts.Users;

public record UpdateUserRequest(
    string FirstName,
    string LastName
    );