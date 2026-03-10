using FluentValidation;
using MyApp.Application.DTOs;

namespace MyApp.Application.Validators
{
    internal class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator() { 
            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
            RuleFor(p => p.Unit)
                .GreaterThanOrEqualTo(0).WithMessage("Unit must be a non-negative integer.");
            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative decimal.");
        }
    }
}
