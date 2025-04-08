namespace Mango.Web.Contracts.Auths;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(e => e.Token).NotEmpty();
        RuleFor(e => e.RefreshToken).NotEmpty();
    }
}
