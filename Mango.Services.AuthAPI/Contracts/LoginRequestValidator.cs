using FluentValidation;

namespace Mango.Services.AuthAPI.Contracts;

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
