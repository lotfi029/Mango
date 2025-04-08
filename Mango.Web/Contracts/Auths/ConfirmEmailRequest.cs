namespace Mango.Web.Contracts.Auths;

public record ConfirmEmailRequest(
    string UserId,
    string Code
    );
