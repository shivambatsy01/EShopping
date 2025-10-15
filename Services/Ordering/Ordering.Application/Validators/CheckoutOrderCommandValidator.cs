using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(e => e.Username)
            .NotEmpty().WithMessage("Username is required")
            .NotNull().WithMessage("Username is required")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters")
            .MinimumLength(2).WithMessage("Username must have at least 2 characters");
        RuleFor(e => e.TotalPrice)
            .NotEmpty().WithMessage("Total Price is required")
            .NotNull().WithMessage("Total Price is required")
            .GreaterThan(-1).WithMessage("Total Price should not be negative");
        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is required");
        RuleFor(e => e.FirstName)
            .NotEmpty().NotNull().WithMessage("First Name is required");
        RuleFor(e => e.LastName)
            .NotEmpty().NotNull().WithMessage("Last Name is required"); 
    }
}