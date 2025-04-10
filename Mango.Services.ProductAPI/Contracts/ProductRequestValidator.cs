using FluentValidation;

namespace Mango.Services.ProductAPI.Contracts;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(2, 100)
            .WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(10, 500)
            .WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(x => x.CategoryName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(2, 50)
            .WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("{PropertyName} must be a valid URL.");
    }
}