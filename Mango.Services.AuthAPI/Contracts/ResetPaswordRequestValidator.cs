using FluentValidation;

namespace Mango.Services.AuthAPI.Contracts;

public class ResetPaswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPaswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric, and Uppercase"); ;

        RuleFor(x => x.ResetCode)
            .NotEmpty();
    }
}