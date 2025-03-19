using FluentValidation;

namespace Mango.Services.AuthAPI.Contracts;

public class ResendConfirmationEmailValidator : AbstractValidator<ResendConfirmationEmailRequest>
{
    public ResendConfirmationEmailValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty();
    }
}
