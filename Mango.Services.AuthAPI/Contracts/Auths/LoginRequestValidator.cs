using FluentValidation;

namespace Store.Services.AuthAPI.Contracts.Auths;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(e => e.Password)
            .NotEmpty();
    }
}
