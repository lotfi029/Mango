namespace Mango.Web.Contracts.Auths;

public record ResetPasswordRequest(
    string Email,
    string ResetCode,
    string NewPassword
);
