namespace Mango.Web.Contracts.Auths;

public record ResetPasswordRequestApi(
    string Email,
    string ResetCode,
    string NewPassword
);
