using FluentValidation;

namespace Mango.Web.Contracts.Auths;

public class ResendConfirmationEmailValidator : AbstractValidator<ResendConfirmationEmailRequest>
{
    public ResendConfirmationEmailValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty();
    }
}
