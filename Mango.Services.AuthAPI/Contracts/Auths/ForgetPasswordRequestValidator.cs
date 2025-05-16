using FluentValidation;

namespace Store.Services.AuthAPI.Contracts.Auths;

public class ForgetPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgetPasswordRequestValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
