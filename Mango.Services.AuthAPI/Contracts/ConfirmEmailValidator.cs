using FluentValidation;

namespace Mango.Services.AuthAPI.Contracts;

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