using FluentValidation;

namespace Store.Services.AuthAPI.Contracts.Auths;

public class ResendConfirmationEmailValidator : AbstractValidator<ResendConfirmationEmailRequest>
{
    public ResendConfirmationEmailValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty();
    }
}
