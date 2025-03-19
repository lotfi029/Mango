namespace Mango.Services.AuthAPI.Contracts;

public record ConfirmEmailRequest(
    string UserId,
    string Code
    );
