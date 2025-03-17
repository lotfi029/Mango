using FluentValidation;

namespace Mango.Services.CouponAPI.Contracts;

public class CouponRequestValidator : AbstractValidator<CouponRequest>
{
    public CouponRequestValidator()
    {
        RuleFor(c => c.DiscountAmount)
            .GreaterThan(0).WithMessage("Discount amount must be greater than zero.");

        RuleFor(c => c.MinAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum amount must be zero or greater.");

        RuleFor(c => c.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(3, 50).WithMessage("Code must be between 3 and 50 characters.");
    }
}
