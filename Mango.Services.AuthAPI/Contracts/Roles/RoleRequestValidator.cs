using FluentValidation;

namespace Store.Services.AuthAPI.Contracts.Roles;

public class RoleRequestValidator : AbstractValidator<RoleRequest>
{
    public RoleRequestValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .Length(3, 200);

        RuleFor(e => e.Permission)
            .NotEmpty();

        RuleFor(e => e.Permission)
            .Must(e => e.Distinct().Count() == e.Count)
            .WithMessage("you should't insert dublicated permission")
            .When(e => e.Permission != null);
    }
}