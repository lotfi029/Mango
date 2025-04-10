namespace Mango.Web.Contracts.Auths;

public record ResetPasswordResponse(
    string Email,
    string Code
);