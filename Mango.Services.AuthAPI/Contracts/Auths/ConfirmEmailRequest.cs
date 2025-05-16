namespace Store.Services.AuthAPI.Contracts.Auths;

public record ConfirmEmailRequest(
    string UserId,
    string Code
    );
