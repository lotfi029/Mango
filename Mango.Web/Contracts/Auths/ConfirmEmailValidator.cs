namespace Mango.Web.Contracts.Auths;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailValidator()
    {
        RuleFor(e => e.Code)
            .NotEmpty();

        RuleFor(e => e.UserId)
            .NotEmpty();
    }
}