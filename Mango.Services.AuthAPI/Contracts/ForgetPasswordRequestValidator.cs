using FluentValidation;

namespace Mango.Services.AuthAPI.Contracts;

public class ForgetPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgetPasswordRequestValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
